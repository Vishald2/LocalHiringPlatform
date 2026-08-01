using LocalHiringPlatform.Domain.Configuration;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces.AI;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace LocalHiringPlatform.Infrastructure.Services.AI
{
    public class GeminiLLMService : ILLMService
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiOptions _options;

        public GeminiLLMService(
            IHttpClientFactory httpClientFactory,
            IOptions<GeminiOptions> options)
        {
            _httpClient = httpClientFactory.CreateClient();
            _options = options.Value;
        }

        public async Task<string> GenerateAsync(
            string prompt)
        {
            var url = BuildUrl(stream: false);

            var requestBody = BuildRequestBody(prompt);

            var response =
                await _httpClient.PostAsJsonAsync(
                    url,
                    requestBody);

            var responseContent =
                await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            var geminiResponse =
                DeserializeResponse(responseContent);

            var aiReply =
                ExtractText(geminiResponse);

            if (string.IsNullOrWhiteSpace(aiReply))
            {
                throw new BusinessException(
                    "AI returned an empty response.");
            }

            aiReply = aiReply
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            return aiReply;
        }

        public async IAsyncEnumerable<string> StreamAsync(
            string prompt,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var url = BuildUrl(stream: true);

            var requestBody = BuildRequestBody(prompt);

           // yield break;

            var request = new HttpRequestMessage(
                                                HttpMethod.Post,
                                                url);

            request.Content = JsonContent.Create(requestBody);

            var response = await _httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            await using var stream =
                await response.Content.ReadAsStreamAsync(cancellationToken);

            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var line = await reader.ReadLineAsync(cancellationToken);

                Console.WriteLine(line);

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (!line.StartsWith("data:"))
                {
                    continue;
                }

                var json = line["data:".Length..].Trim();

                if (string.IsNullOrWhiteSpace(json))
                {
                    continue;
                }

                var geminiResponse = DeserializeResponse(json);

                var token = ExtractText(geminiResponse);

                if (!string.IsNullOrWhiteSpace(token))
                {
                    yield return token;
                }
            }
        }

        private object BuildRequestBody(string prompt)
        {
            return new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new
                    {
                        text = prompt
                    }
                }
            }
        }
            };
        }

        private string BuildUrl(bool stream)
        {
            if (stream)
            {
                return $"{_options.GeminiStreamingEndpoint}{_options.ApiKey}";
            }

            return $"{_options.GeminiEndpoint}{_options.ApiKey}";
        }

        private GeminiResponse? DeserializeResponse(string json)
        {
            return JsonSerializer.Deserialize<GeminiResponse>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        private static string? ExtractText(
                GeminiResponse? response)
        {
            return response?
                .Candidates
                .FirstOrDefault()?
                .Content
                .Parts
                .FirstOrDefault()?
                .Text;
        }
    }
}