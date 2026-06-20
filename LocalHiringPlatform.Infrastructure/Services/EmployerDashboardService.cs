using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class EmployerDashboardService : IEmployerDashboardService
    {
        private readonly IEmployerProfileRepository
            _employerProfileRepository;

        private readonly IJobRepository
            _jobRepository;

        private readonly IJobApplicationRepository
            _jobApplicationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployerDashboardService(
            IEmployerProfileRepository employerProfileRepository,
            IJobRepository jobRepository,
            IJobApplicationRepository jobApplicationRepository,
            IUnitOfWork unitOfWork)
        {
            _employerProfileRepository =
                employerProfileRepository;

            _jobRepository =
                jobRepository;

            _jobApplicationRepository =
                jobApplicationRepository;

            _unitOfWork =
                unitOfWork;
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

        public async Task<EmployerProfileModel?> GetProfileAsync(Guid userId)
        {
            var employerProfile =
                await _employerProfileRepository
                    .GetByUserIdAsync(userId);
            if(employerProfile == null)
                return null;
            else
                return new EmployerProfileModel
                {
                    //Id = employerProfile.EntityId,
                    //UserId = employerProfile.UserId,
                    CompanyName = employerProfile.CompanyName,
                    Industry = employerProfile.Industry,
                    Website = employerProfile.Website,
                    CompanyDescription = employerProfile.CompanyDescription,
                    IsEmailVerified = employerProfile.User?.EmailVerified ?? false
                };
        }

        public async Task UpdateProfileAsync(
            Guid userId, EmployerProfileModel profile)
        {
            var employerProfile =
                await _employerProfileRepository
                    .GetByUserIdAsync(userId);

            if (employerProfile == null)
            {
                throw new BusinessException(
                    "Employer profile not found.");
            }

            employerProfile.CompanyName =
                profile.CompanyName;

            employerProfile.Industry =
                profile.Industry;

            employerProfile.Website =
                profile.Website;

            employerProfile.CompanyDescription =
                profile.CompanyDescription;

            await _unitOfWork
                .SaveChangesAsync();
        }
    }
}