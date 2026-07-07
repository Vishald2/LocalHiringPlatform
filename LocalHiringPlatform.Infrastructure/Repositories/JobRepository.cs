using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Domain.Models.AI;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalHiringPlatform.Infrastructure.Repositories;

public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _dbContext;

    public JobRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Job job)
    {
        await _dbContext.Jobs.AddAsync(job);
    }

    public async Task<List<Job>> GetAllAsync()
    {
        return await _dbContext.Jobs
            .ToListAsync();
    }

    public async Task<List<Job>> GetByEmployerProfileIdAsync(Guid employerProfileId)
    {
        var abc = await _dbContext.Jobs
                        .Include(x => x.JobApplications)
                        .Where(x =>
                            x.EmployerProfileId ==
                            employerProfileId)
                        .ToListAsync();
        return abc;
    }

    public async Task<Job?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Jobs
            .FirstOrDefaultAsync(
                x => x.EntityId == id);
    }

    public Task UpdateAsync(Job job)
    {
        _dbContext.Jobs.Update(job);

        return Task.CompletedTask;
    }

    public async Task<List<Job>>SearchAsync(string? keyword, string? city)
    {
        var jobs = await _dbContext.Jobs.ToListAsync();

        IQueryable<Job> query =
            _dbContext.Jobs
                .Where(x => x.IsActive);

        keyword = keyword?.Trim().ToLower();
        city = city?.Trim().ToLower();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(
                x =>
                    x.Title.ToLower().Contains(keyword)
                    ||
                    (x.Description ?? "")
                        .ToLower()
                        .Contains(keyword)
                    ||
                    (x.RequiredSkills ?? "")
                        .ToLower()
                        .Contains(keyword));
        }

        if (!string.IsNullOrWhiteSpace(city))
        {
            query = query.Where(
                x =>
                    (x.City ?? "")
                        .ToLower()
                        .Contains(city));
        }

        //  return result;

        return await query
            .ToListAsync();
    }

    /*AI SEARCHING CODE. START*/
    public async Task<List<JobSearchResultModel>> SearchAsync(JobSearchModel model)
    {
        var jobs =
            await _dbContext.Jobs
                .Where(j => j.IsActive)
                .ToListAsync();

        var results = new List<JobSearchResultModel>();

        foreach (var job in jobs)
        {
            // Experience Filter
            if (!IsExperienceMatch(job, model))
                continue;

            // Salary Filter
            if (!IsSalaryMatch(job, model))
                continue;

            int matchScore = 0;

            // City (Mandatory filter if specified)
            if (model.City.Any())
            {
                if (!IsLocationMatch(job.City, model.City))
                    continue;

                matchScore += 30;
            }

            // State (Mandatory filter if specified)
            if (model.State.Any())
            {
                if (!IsLocationMatch(job.State, model.State))
                    continue;

                matchScore += 20;
            }

            // Skills (Mandatory OR match if specified)
            if (model.RequiredSkills.Any())
            {
                int skillScore =
                    CalculateSkillScore(
                        job.RequiredSkills,
                        model.RequiredSkills);

                if (skillScore == 0)
                    continue;

                matchScore += skillScore;
            }

            // Experience matched
            if (!string.IsNullOrWhiteSpace(model.MinExperienceRequired) ||
                !string.IsNullOrWhiteSpace(model.MaxExperienceRequired))
            {
                matchScore += 15;
            }

            // Salary matched
            if (!string.IsNullOrWhiteSpace(model.MinSalary) ||
                !string.IsNullOrWhiteSpace(model.MaxSalary))
            {
                matchScore += 10;
            }

            // Experience (Mandatory filter if specified)
            if (!string.IsNullOrWhiteSpace(model.MinExperienceRequired) ||
                !string.IsNullOrWhiteSpace(model.MaxExperienceRequired))
            {
                if (!IsExperienceMatch(job, model))
                    continue;

                matchScore += 10;
            }

            results.Add(new JobSearchResultModel
            {
                Job = new JobModel
                {
                    ApplicantCount = 0,
                    City = job.City,
                    Description = job.Description,
                    EntityId = job.EntityId,
                    ExperienceRequired = job.ExperienceRequired,
                    IsActive = job.IsActive,
                    MaxExperienceRequired = job.MaxExperienceRequired,
                    MaxSalary = job.MaxSalary,
                    MinSalary = job.MinSalary,
                    RequiredSkills = job.RequiredSkills,
                    State = job.State,
                    Title = job.Title,
                    CreatedOn = job.CreatedOn,
                },
                MatchScore = matchScore
            });
        }

        return results
            .OrderByDescending(r => r.MatchScore)
            .ThenByDescending(r => r.Job.CreatedOn)
            .ToList();
    }

    private bool IsExperienceMatch(Job job, JobSearchModel model)
    {
        int? minExperience =
            ParseInt(model.MinExperienceRequired);

        int? maxExperience =
            ParseInt(model.MaxExperienceRequired);

        if (minExperience == null &&
            maxExperience == null)
        {
            return true;
        }

        int jobMin = job.ExperienceRequired;
        int jobMax = job.MaxExperienceRequired ?? jobMin;

        if (minExperience != null &&
            jobMax < minExperience.Value)
        {
            return false;
        }

        if (maxExperience != null &&
            jobMin > maxExperience.Value)
        {
            return false;
        }

        return true;
    }

    private bool IsSalaryMatch(Job job, JobSearchModel model)
    {
        decimal? minSalary =
            ParseDecimal(model.MinSalary);

        decimal? maxSalary =
            ParseDecimal(model.MaxSalary);

        if (minSalary == null &&
            maxSalary == null)
        {
            return true;
        }

        decimal jobMin = job.MinSalary ?? 0;
        decimal jobMax = job.MaxSalary ?? decimal.MaxValue;

        if (minSalary != null &&
            jobMax < minSalary.Value)
        {
            return false;
        }

        if (maxSalary != null &&
            jobMin > maxSalary.Value)
        {
            return false;
        }

        return true;
    }

    private bool IsLocationMatch(
        string jobValue,
        List<string> searchValues)
    {
        if (searchValues == null ||
            searchValues.Count == 0)
        {
            return false;
        }

        var jobValues =
            SplitValues(jobValue);

        foreach (var searchValue in searchValues)
        {
            if (jobValues.Any(j =>
                j.Equals(searchValue,
                StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
        }

        return false;
    }

    private int CalculateSkillScore(
        string? requiredSkills,
        List<string> searchSkills)
    {
        if (searchSkills == null ||
            searchSkills.Count == 0)
        {
            return 0;
        }

        int score = 0;

        var jobSkills =
            SplitValues(requiredSkills);

        foreach (var searchSkill in searchSkills)
        {
            if (jobSkills.Any(s =>
                s.Equals(searchSkill,
                StringComparison.OrdinalIgnoreCase)))
            {
                score += 50;
            }
        }

        return score;
    }

    private List<string> SplitValues(
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new List<string>();
        }

        return value
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(v => v.Trim())
            .ToList();
    }

    private int? ParseInt(
        string? value)
    {
        if (int.TryParse(value, out int result))
        {
            return result;
        }

        return null;
    }

    private decimal? ParseDecimal(
        string? value)
    {
        if (decimal.TryParse(value, out decimal result))
        {
            return result;
        }

        return null;
    }
    /*AI SEARCHING CODE. END*/
}