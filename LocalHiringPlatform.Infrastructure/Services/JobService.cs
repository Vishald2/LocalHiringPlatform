using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;

namespace LocalHiringPlatform.Infrastructure.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;
    private readonly IEmployerProfileRepository
        _employerProfileRepository;

    private readonly IUnitOfWork _unitOfWork;

    public JobService(
        IJobRepository jobRepository,
        IEmployerProfileRepository
            employerProfileRepository,
        IUnitOfWork unitOfWork)
    {
        _jobRepository = jobRepository;

        _employerProfileRepository =
            employerProfileRepository;

        _unitOfWork = unitOfWork;
    }

    public async Task AddJobAsync(
        CreateJobModel model,
        Guid userId)
    {
        var employerProfile =
            await _employerProfileRepository
                .GetByUserIdAsync(userId);

        if (employerProfile == null)
        {
            throw new BusinessException(
                "Employer profile not found");
        }

        var job = new Job
        {
            EmployerProfileId =
                employerProfile.EntityId,

            Title = model.Title,
            Description = model.Description,
            City = model.City,
            State = model.State,
            MinSalary = model.MinSalary,
            MaxSalary = model.MaxSalary,
            ExperienceRequired =
                model.ExperienceRequired,

            RequiredSkills =
                model.RequiredSkills,

            IsActive = true
        };

        await _jobRepository
            .AddAsync(job);

        await _unitOfWork
            .SaveChangesAsync();
    }

    public async Task<List<JobModel>> GetAllJobsAsync()
    {
        var jobs =
            await _jobRepository
                .GetAllAsync();

        return jobs
            .Select(job => new JobModel
            {
                EntityId = job.EntityId,
                Title = job.Title,
                Description = job.Description,
                City = job.City,
                State = job.State,
                MinSalary = job.MinSalary,
                MaxSalary = job.MaxSalary,
                ExperienceRequired = job.ExperienceRequired,
                RequiredSkills = job.RequiredSkills,
                IsActive = job.IsActive
            })
            .ToList();
    }

    public async Task<List<JobModel>> GetEmployerJobsAsync(Guid userId)
    {
        var employerProfile =
            await _employerProfileRepository.GetByUserIdAsync(userId);

        if (employerProfile == null)
        {
            throw new Exception(
                "Employer profile not found.");
        }

        var jobs =
            await _jobRepository
                .GetByEmployerProfileIdAsync(
                    employerProfile.EntityId);

        return jobs.Select(j => new JobModel
        {
            EntityId = j.EntityId,

            Title = j.Title,

            Description = j.Description,

            City = j.City,

            State = j.State,

            MinSalary = j.MinSalary,

            MaxSalary = j.MaxSalary,

            ExperienceRequired = j.ExperienceRequired,

            RequiredSkills = j.RequiredSkills,

            IsActive = j.IsActive
        }).ToList();
    }

    public async Task<JobModel?> GetJobAsync(Guid id)
    {
        var job =
            await _jobRepository
                .GetByIdAsync(id);

        if (job == null)
        {
            return null;
        }

        return new JobModel
        {
            EntityId = job.EntityId,
            Title = job.Title,
            Description = job.Description,
            City = job.City,
            State = job.State,
            MinSalary = job.MinSalary,
            MaxSalary = job.MaxSalary,
            ExperienceRequired =
                job.ExperienceRequired,

            RequiredSkills =
                job.RequiredSkills,

            IsActive = job.IsActive
        };
    }

    public async Task UpdateJobAsync(Guid userId, UpdateJobModel model)
    {
        var employerProfile = await _employerProfileRepository
                .GetByUserIdAsync(userId);

        if (employerProfile == null)
        {
            throw new Exception("Employer profile not found.");
        }

        var job = await _jobRepository.GetByIdAsync(model.EntityId);

        if (job == null)
        {
            throw new Exception("Job not found.");
        }

        if (job.EmployerProfileId != employerProfile.EntityId)
        {
            throw new UnauthorizedAccessException(
                "You cannot edit this job.");
        }

        job.Title =
            model.Title;

        job.Description =
            model.Description;

        job.City =
            model.City;

        job.State =
            model.State;

        job.MinSalary =
            model.MinSalary;

        job.MaxSalary =
            model.MaxSalary;

        job.ExperienceRequired =
            model.ExperienceRequired;

        job.RequiredSkills =
            model.RequiredSkills;

        job.IsActive =
            model.IsActive;

        await _jobRepository
            .UpdateAsync(job);

        await _unitOfWork
            .SaveChangesAsync();
    }
}