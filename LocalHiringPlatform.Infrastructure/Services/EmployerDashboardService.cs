using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class EmployerDashboardService
        : IEmployerDashboardService
    {
        private readonly IEmployerProfileRepository
            _employerProfileRepository;

        private readonly IJobRepository
            _jobRepository;

        private readonly IJobApplicationRepository
            _jobApplicationRepository;

        public EmployerDashboardService(
            IEmployerProfileRepository employerProfileRepository,
            IJobRepository jobRepository,
            IJobApplicationRepository jobApplicationRepository)
        {
            _employerProfileRepository =
                employerProfileRepository;

            _jobRepository =
                jobRepository;

            _jobApplicationRepository =
                jobApplicationRepository;
        }

        public async Task<EmployerDashboardModel>  GetDashboardAsync(Guid userId)
        {
            var employerProfile =
                await _employerProfileRepository
                    .GetByUserIdAsync(userId);

            if (employerProfile == null)
            {
                throw new Exception(
                    "Employer profile not found.");
            }

            var jobs =
                await _jobRepository
                    .GetByEmployerProfileIdAsync(
                        employerProfile.EntityId);

            var totalApplicants = 0;

            foreach (var job in jobs)
            {
                var applications =
                    await _jobApplicationRepository
                        .GetByJobIdAsync(
                            job.EntityId);

                totalApplicants +=
                    applications.Count;
            }

            return new EmployerDashboardModel
            {
                TotalJobs =
                    jobs.Count,

                ActiveJobs =
                    jobs.Count(j => j.IsActive),

                TotalApplicants =
                    totalApplicants,

                NewApplications = 0
            };
        }
    }
}