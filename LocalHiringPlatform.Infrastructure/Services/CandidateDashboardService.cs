using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class CandidateDashboardService : ICandidateDashboardService
    {
        ICandidateProfileRepository _candidateProfileRepository;
        IJobApplicationRepository _jobApplicationRepository;
        public CandidateDashboardService(
            ICandidateProfileRepository candidateProfileRepository,
            IJobApplicationRepository jobApplicationRepository
         )
        {
            _candidateProfileRepository = candidateProfileRepository;
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task<CandidateDashboardModel>
            GetDashboardAsync(Guid userId)
        {
            var profile =
                await _candidateProfileRepository
                    .GetByUserIdAsync(userId);

            if (profile == null)
            {
                throw new Exception(
                    "Candidate profile not found.");
            }

            var applications =
                await _jobApplicationRepository
                    .GetByCandidateProfileIdAsync(
                        profile.EntityId);

            return new CandidateDashboardModel
            {
                TotalApplications =
                    applications.Count,

                Shortlisted =
                    applications.Count(
                        x => x.Status == "Shortlisted"),

                InterviewScheduled =
                    applications.Count(
                        x => x.Status ==
                            "Interview Scheduled"),

                Rejected =
                    applications.Count(
                        x => x.Status == "Rejected"),

                Hired =
                    applications.Count(
                        x => x.Status == "Hired")
            };
        }
    }
}
