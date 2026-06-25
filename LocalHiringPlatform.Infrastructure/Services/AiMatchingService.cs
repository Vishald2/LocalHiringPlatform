using LocalHiringPlatform.Domain.Configuration;
using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Infrastructure.Helpers;
using LocalHiringPlatform.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class AiMatchingService : IAiMatchingService
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiOptions _options;
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateProfileRepository _candidateProfileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAiAnalysisRepository _aiAnalysisRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AiMatchingService(
                IHttpClientFactory httpClientFactory,
                IOptions<GeminiOptions> options,
                IJobRepository jobRepository,
                ICandidateProfileRepository candidateProfileRepository,
                IUserRepository userRepository,
                IAiAnalysisRepository aiAnalysisRepository,
                IJobApplicationRepository jobApplicationRepository,
                IUnitOfWork unitOfWork
            )
        {
            _httpClient = httpClientFactory.CreateClient();
            _options = options.Value;
            _jobRepository = jobRepository;
            _candidateProfileRepository = candidateProfileRepository;
            _userRepository = userRepository;
            _aiAnalysisRepository = aiAnalysisRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _unitOfWork = unitOfWork;
        }

        string GetResumeText(CandidateProfile candidateProfile)
        {
            string resumeText = string.Empty;

            if (!string.IsNullOrEmpty(
                    candidateProfile.ResumeFilePath))
            {
                var physicalPath =
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        candidateProfile.ResumeFilePath
                            .TrimStart('/')
                            .Replace(
                                '/',
                                Path.DirectorySeparatorChar));

                if (File.Exists(
                        physicalPath))
                {
                    try
                    {
                        resumeText =
                            PdfHelper.ExtractText(
                                physicalPath);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
            }

            return resumeText;
        }

        public async Task<AiMatchResultModel> AnalyzeAsync(
                    Guid jobId, Guid candidateProfileId, bool reanalyse)
        {
            string jobDescription, candidateProfileSummary, candidateSkills, candidateSkillsSummary;

            /*WILL BE USED TO CHECK IN DB IF ReanalyseAllowed*/
            bool isReanalyseAllowed = true;

            CandidateProfile? candidateProfile = await _candidateProfileRepository.GetByProfileIdAsync(candidateProfileId);

            Job? job = await _jobRepository.GetByIdAsync(jobId);

            if (candidateProfile == null || job == null)
            {
                throw new BusinessException("Invalid job or candidate profile.");
            }

            var jobApplication = await _jobApplicationRepository
                .GetByJobAndCandidateAsync(
                    jobId,
                    candidateProfileId);

            if (jobApplication == null)
            {
                throw new BusinessException(
                    "Job application not found.");
            }

            var existingAnalysis = await _aiAnalysisRepository
                                    .GetByJobApplicationIdAsync(
                                    jobApplication.EntityId);

            int analysisCount = 0;

            if (existingAnalysis != null)
            {
                analysisCount = existingAnalysis.AnalysisCount;
            }

            if (analysisCount >= 3) {
                isReanalyseAllowed = false;
            }


            if (existingAnalysis != null && (reanalyse == false
                                                || isReanalyseAllowed == false)
                )
            {
                return new AiMatchResultModel
                {
                    Score =
                        existingAnalysis.Score,

                    Recommendation =
                        existingAnalysis.Recommendation,

                    Strengths =
                        JsonSerializer.Deserialize<
                            List<string>>(
                            existingAnalysis.Strengths)
                        ?? new List<string>(),

                    Gaps =
                        JsonSerializer.Deserialize<
                            List<string>>(
                            existingAnalysis.Gaps)
                        ?? new List<string>()
                };
            }

            jobDescription = job.Description;
            candidateProfileSummary = candidateProfile.ProfileSummary;
            candidateSkills = string.Join(", ", candidateProfile.CandidateSkills.Select(cs => cs.Skill.SkillName));
            candidateSkillsSummary = candidateProfileSummary + ", Skills: " + candidateSkills;

            string resumeText = GetResumeText(candidateProfile);

            candidateSkillsSummary = candidateSkillsSummary + ", Resume Text:- " + resumeText;

            var url = $"{_options.GeminiEndpoint}{_options.ApiKey}";

            var filePath =
                Path.Combine(Directory.GetCurrentDirectory(),
                "Prompts", "CandidateMatchingPrompt.txt");

            var promptTemplate =
                await File.ReadAllTextAsync(
                    filePath);

            var prompt =
                promptTemplate
                    .Replace(
                        "{{JOB_DESCRIPTION}}",
                            jobDescription
                        )
                    .Replace(
                        "{{CANDIDATE_PROFILE}}",
                            candidateSkillsSummary
                        );

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

            var result = JsonSerializer.Deserialize<AiMatchResultModel>(
                    aiJson!,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });



            var aiAnalysis =
                new AiAnalysis
                {
                    JobApplicationId =
                        jobApplication.EntityId,

                    Score =
                        result!.Score,

                    Recommendation =
                        result.Recommendation,

                    Strengths =
                        JsonSerializer.Serialize(
                            result.Strengths),

                    Gaps =
                        JsonSerializer.Serialize(
                            result.Gaps),

                    AnalyzedOn =
                        DateTime.UtcNow,

                    AnalysisCount = analysisCount + 1,
                };

            if (existingAnalysis == null)
            {
                await _aiAnalysisRepository
                    .AddAsync(aiAnalysis);
            }
            else
            {
                existingAnalysis.Score = result.Score;
                existingAnalysis.Recommendation = result.Recommendation;
                existingAnalysis.Strengths =
                    JsonSerializer.Serialize(result.Strengths);
                existingAnalysis.Gaps =
                    JsonSerializer.Serialize(result.Gaps);
                existingAnalysis.AnalyzedOn =
                    DateTime.UtcNow;
                existingAnalysis.AnalysisCount =
                    analysisCount + 1;

                _aiAnalysisRepository
                    .Update(existingAnalysis);
            }

            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork
                .SaveChangesAsync();

            return result!;
        }
    }
}
