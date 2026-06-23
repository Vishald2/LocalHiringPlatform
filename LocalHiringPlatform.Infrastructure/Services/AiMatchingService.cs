using LocalHiringPlatform.Domain.Configuration;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class AiMatchingService : IAiMatchingService
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiOptions _options;
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateProfileRepository _candidateProfileRepository;
        private readonly IUserRepository _userRepository;

        public AiMatchingService(
                IHttpClientFactory httpClientFactory,
                IOptions<GeminiOptions> options,
                IJobRepository jobRepository,
                ICandidateProfileRepository candidateProfileRepository  ,
                IUserRepository userRepository
            )
        {
            _httpClient = httpClientFactory.CreateClient();
            _options = options.Value;
            _jobRepository = jobRepository;
            _candidateProfileRepository = candidateProfileRepository;
            _userRepository = userRepository;
        }

        public async Task<AiMatchResultModel> AnalyzeAsync(
                    Guid jobId,
                    Guid candidateProfileId)
        {
            string jobDescription, candidateProfileSummary, candidateSkills, candidateSkillsSummary;

            CandidateProfile? candidateProfile = await _candidateProfileRepository.GetByProfileIdAsync(candidateProfileId);

            Job? job = await _jobRepository.GetByIdAsync(jobId);

            if(candidateProfile == null || job == null)
            {
                throw new BusinessException("Invalid job or candidate profile.");
            }

            jobDescription = job.Description;

            candidateProfileSummary = candidateProfile.ProfileSummary;
            candidateSkills = string.Join(", ", candidateProfile.CandidateSkills.Select(cs => cs.Skill.SkillName));
            
            candidateSkillsSummary =  candidateProfileSummary + ", Skills: " + candidateSkills;

            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_options.ApiKey}";

            var prompt =
    "You are an expert recruiter.\n\n" +

    $"Job Description:\n{jobDescription}\n\n" +

    $"Candidate Profile:\n{candidateSkillsSummary}\n\n" +

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

            var response = await _httpClient.PostAsJsonAsync(
                    url,
                    requestBody);

            var responseContent =
                await response.Content.ReadAsStringAsync();

            var geminiResponse =
                JsonSerializer.Deserialize<GeminiResponse>(
                    responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            var aiJson =
                geminiResponse?
                    .Candidates
                    .FirstOrDefault()?
                    .Content
                    .Parts
                    .FirstOrDefault()?
                    .Text;

            aiJson = aiJson?
                    .Replace("```json", "")
                    .Replace("```", "")
                    .Trim();

            if (string.IsNullOrWhiteSpace(aiJson))
            {
                throw new BusinessException("AI service returned an empty response.");
            }

            var result =
                JsonSerializer.Deserialize<AiMatchResultModel>(
                    aiJson!,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return result!;
        }
    }
}
