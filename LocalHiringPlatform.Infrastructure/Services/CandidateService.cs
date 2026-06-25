using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class CandidateService: ICandidateService
    {
        private readonly ICandidateProfileRepository _candidateProfileRepository;
        private readonly IJobRepository _jobRepository;


        public CandidateService(
            ICandidateProfileRepository candidateProfileRepository,
            IJobRepository jobRepository
            )
        {
            _candidateProfileRepository = candidateProfileRepository;
            _jobRepository = jobRepository;
        }

        public async Task<List<RecommendedJobModel>>
    GetRecommendedJobsAsync(
        Guid userId)
        {
            var candidateProfile =
                await _candidateProfileRepository
                    .GetByUserIdAsync(userId);

            if (candidateProfile == null)
            {
                throw new BusinessException(
                    "Candidate profile not found.");
            }

            var candidateSkills =
                candidateProfile
                    .CandidateSkills
                    .Select(x => x.Skill.SkillName)
                    .ToList();

            var jobs =
                await _jobRepository
                    .GetAllAsync();

            var recommendedJobs =
                jobs
                .Where(x => x.IsActive)
                .Select(job =>
                {
                    var requiredSkills =
                        (job.RequiredSkills ?? "")
                        .Split(
                            ',',
                            StringSplitOptions
                                .RemoveEmptyEntries)
                        .Select(x => x.Trim())
                        .ToList();

                    int matchPercentage = 0;

                    if (requiredSkills.Any())
                    {
                        int matchedSkills =
                            candidateSkills.Count(
                                cs =>
                                    requiredSkills.Any(
                                        rs =>
                                            rs.Equals(
                                                cs,
                                                StringComparison
                                                    .OrdinalIgnoreCase)));

                        matchPercentage =
                            (matchedSkills * 100)
                            / requiredSkills.Count;
                    }

                    return new RecommendedJobModel
                    {
                        JobId =
                            job.EntityId,

                        Title =
                            job.Title,

                        City =
                            job.City,

                        State =
                            job.State,

                        MinSalary =
                            job.MinSalary,

                        MaxSalary =
                            job.MaxSalary,

                        MatchPercentage =
                            matchPercentage
                    };
                })
                .OrderByDescending(
                    x => x.MatchPercentage)
                .ToList();

            return recommendedJobs;
        }
    }
}
