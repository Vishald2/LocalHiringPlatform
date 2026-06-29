using LocalHiringPlatform.Domain.Configuration;
using LocalHiringPlatform.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LocalHiringPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeminiTestController
    : ControllerBase
    {
        private readonly HttpClient _httpClient;

        private readonly GeminiOptions _options;

        private readonly IAiMatchingService _aiMatchingService;

        public GeminiTestController(
                IHttpClientFactory httpClientFactory,
                IOptions<GeminiOptions> options,
                IAiMatchingService aiMatchingService
        )
        {
            _httpClient =
                httpClientFactory.CreateClient();

            _options =
                options.Value;

            _aiMatchingService =
                aiMatchingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_options.ApiKey}";

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
                                    text =
                                        "What is 2 + 2? Reply with only the answer."
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

            return Ok(responseContent);
        }

        [HttpGet("match")]
        public async Task<IActionResult> Match(Guid jobId, Guid candidateProfileId, bool reanalyse)
        {
            try
            {
                var result =
                    await _aiMatchingService.AnalyzeAsync(jobId, candidateProfileId, reanalyse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
