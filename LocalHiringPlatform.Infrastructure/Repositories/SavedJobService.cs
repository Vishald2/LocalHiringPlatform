using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Repositories
{
    public class SavedJobService : ISavedJobService
    {
        ISavedJobRepository _savedJobRepository;
        IJobRepository _jobRepository;
        IUnitOfWork _unitOfWork;

        public SavedJobService(ISavedJobRepository savedJobRepository,
            IJobRepository jobRepository,
            IUnitOfWork unitOfWork)
        {
            _savedJobRepository = savedJobRepository;
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task SaveJobAsync(
    Guid userId,
    Guid jobId)
        {
            var existing =
                await _savedJobRepository
                    .GetAsync(
                        userId,
                        jobId);

            if (existing != null)
            {
                throw new BusinessException(
                    "Job already saved.");
            }

            await _savedJobRepository
                .AddAsync(
                    new SavedJob
                    {
                        UserId = userId,
                        JobId = jobId
                    });

            await _unitOfWork
                .SaveChangesAsync();
        }

        public async Task RemoveSavedJobAsync(
    Guid userId,
    Guid jobId)
        {
            var savedJob =
                await _savedJobRepository
                    .GetAsync(
                        userId,
                        jobId);

            if (savedJob == null)
            {
                return;
            }

            _savedJobRepository
                .Remove(savedJob);

            await _unitOfWork
                .SaveChangesAsync();
        }

        public async Task<List<JobModel>>
    GetMySavedJobsAsync(
        Guid userId)
        {
            var savedJobs =
                await _savedJobRepository
                    .GetByUserIdAsync(
                        userId);

            return savedJobs
                .Select(
                    x => new JobModel
                    {
                        EntityId =
                            x.Job.EntityId,

                        Title =
                            x.Job.Title,

                        Description =
                            x.Job.Description,

                        City =
                            x.Job.City,

                        State =
                            x.Job.State,

                        MinSalary =
                            x.Job.MinSalary,

                        MaxSalary =
                            x.Job.MaxSalary,

                        ExperienceRequired =
                            x.Job.ExperienceRequired,

                        RequiredSkills =
                            x.Job.RequiredSkills,

                        IsActive =
                            x.Job.IsActive
                    })
                .ToList();
        }

    }
}
