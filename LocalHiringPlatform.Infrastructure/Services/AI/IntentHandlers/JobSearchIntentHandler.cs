using Azure.Core;
using LocalHiringPlatform.Domain.Enums;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Interfaces.AI.LocalHiringPlatform.Domain.Interfaces.AI;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Domain.Models.AI;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services.AI.IntentHandlers
{
    public class JobSearchIntentHandler : IAIIntentHandler
    {
        public AIIntentType IntentType => AIIntentType.JobSearch;

        private readonly ILLMService _llmService;
        private readonly IPromptService _promptService;
        private readonly IJobService _jobService;
        public JobSearchIntentHandler(IPromptService promptService, ILLMService llmService,
            IJobService jobService)
        {
            _llmService = llmService;
            _promptService = promptService;
            _jobService = jobService;
        }

        public Task<AIIntentHandlerResponse> HandleAsync(AIIntentModel intentModel, AIChatRequestModel request)
        {
            var promptTemplate =
                 _promptService.GetPromptAsync(
                    "JobSearchIntentPrompt.txt").Result;

            var prompt =
                $"{promptTemplate}\n\nUser Request:\n{request.Message}";

            var aiReply = 
                 _llmService.GenerateAsync(
                    prompt).Result;

            JobSearchAIModel? jobSearchAIModel = null;

            try
            {
                jobSearchAIModel = JsonSerializer.Deserialize<JobSearchAIModel>(aiReply);
            }
            catch
            {
                jobSearchAIModel = null;
            }

            JobSearchModel? jobSearchModel = null;

            if (jobSearchAIModel != null)
            {
                jobSearchModel = new JobSearchModel
                {
                    City = jobSearchAIModel?.City ?? new List<string>(),
                    State = jobSearchAIModel?.State ?? new List<string>(),
                    RequiredSkills = jobSearchAIModel?.RequiredSkills ?? new List<string>(),
                    MinExperienceRequired = jobSearchAIModel?.MinExperienceRequired,
                    MaxExperienceRequired = jobSearchAIModel?.MaxExperienceRequired,
                    MinSalary = jobSearchAIModel?.MinSalary,
                    MaxSalary = jobSearchAIModel?.MaxSalary
                };
            }

            bool hasCity =
                        jobSearchModel.City != null &&
                        jobSearchModel.City.Any();

            bool hasSkill =
                jobSearchModel.RequiredSkills != null &&
                jobSearchModel.RequiredSkills.Any();

            if (!hasCity || !hasSkill)
            {
                return Task.FromResult(new AIIntentHandlerResponse
                {
                    Intent = AIIntentType.JobSearch.ToString(),
                    Data = null,
                    Message =
                        "Please specify at least one city and one skill to search for jobs.\n\n" +
                        "Examples:\n" +
                        "• .NET jobs in Delhi\n" +
                        "• Angular jobs in Panipat\n" +
                        "• React developer in Gurgaon"
                });
            }

            List<JobSearchResultModel>? jobSearchResultModels = null;

            if (jobSearchModel != null)
            {
                // Use the job service to search for jobs
                jobSearchResultModels = _jobService.SearchAsync(jobSearchModel).Result;
                // Process the results as needed
            }

            string msg = jobSearchResultModels != null && jobSearchResultModels.Any()
                    ? $"Found {jobSearchResultModels.Count} job(s) matching your criteria."
                    : "No jobs found matching your criteria.";

            return Task.FromResult(new AIIntentHandlerResponse
            {
                Intent = IntentType.ToString(),
                Message = msg,
                Data = jobSearchResultModels
            });
        }
    }
}
