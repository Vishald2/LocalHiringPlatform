using LocalHiringPlatform.Domain.Configuration;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class AiMatchingService
        : IAiMatchingService
    {
        private readonly HttpClient _httpClient;

        private readonly GeminiOptions _options;

        public AiMatchingService(
            IHttpClientFactory httpClientFactory,
            IOptions<GeminiOptions> options)
        {
            _httpClient =
                httpClientFactory.CreateClient();

            _options =
                options.Value;
        }

        public async Task<AiMatchResultModel>
    AnalyzeAsync(
        string jobDescription,
        string candidateProfile)
        {
            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_options.ApiKey}";

            var prompt =
    "You are an expert recruiter.\n\n" +

    $"Job Description:\n{jobDescription}\n\n" +

    $"Candidate Profile:\n{candidateProfile}\n\n" +

    "Return ONLY valid JSON.\n\n" +

    "{\n" +
    "  \"score\": 80,\n" +
    "  \"recommendation\": \"Interview\",\n" +
    "  \"strengths\": [\n" +
    "    \"Strong ASP.NET Core experience\"\n" +
    "  ],\n" +
    "  \"gaps\": [\n" +
    "    \"No Azure experience\"\n" +
    "  ]\n" +
    "}";
            var requestBody =
                new
                {
                    contents =
                        new[]
                        {
                    new
                    {
                        parts =
                            new[]
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

            return new AiMatchResultModel
            {
                Score = 0,
                Recommendation = responseContent
            };
        }
    }
}
