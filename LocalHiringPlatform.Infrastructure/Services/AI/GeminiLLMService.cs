using LocalHiringPlatform.Domain.Configuration;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces.AI;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
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
            var url =
                $"{_options.GeminiEndpoint}{_options.ApiKey}";

            var requestBody =
                new
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
                JsonSerializer.Deserialize<GeminiResponse>(
                    responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            var aiReply =
                geminiResponse?
                    .Candidates
                    .FirstOrDefault()?
                    .Content
                    .Parts
                    .FirstOrDefault()?
                    .Text;

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
    }
}