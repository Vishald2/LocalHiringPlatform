using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Repositories.EducationRepositories
{
    public class CandidateEducationRepository : ICandidateEducationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CandidateEducationRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CandidateEducation>> GetByCandidateProfileIdAsync(
            Guid candidateProfileId)
        {
            return await _dbContext.CandidateEducations
                .Where(x => x.CandidateProfileId == candidateProfileId)
                .Include(x => x.Education)
                .Include(x => x.Course)
                .Include(x => x.University)
                .Include(x => x.CandidateEducationSpecializations)
                    .ThenInclude(x => x.CourseSpecialization)
                        .ThenInclude(x => x.Specialization)
                .OrderByDescending(x => x.IsHighestEducation)
                .ThenByDescending(x => x.EndYear)
                .ToListAsync();
        }

        public async Task<CandidateEducation?> GetByEntityIdAsync(
            Guid candidateEducationEntityId)
        {
            return await _dbContext.CandidateEducations
                .Include(x => x.CandidateEducationSpecializations)
                .FirstOrDefaultAsync(x =>
                    x.EntityId == candidateEducationEntityId);
        }

        public async Task AddAsync(
            CandidateEducation candidateEducation)
        {
            await _dbContext.CandidateEducations
                .AddAsync(candidateEducation);
        }

        public void Update(
            CandidateEducation candidateEducation)
        {
            _dbContext.CandidateEducations
                .Update(candidateEducation);
        }

        public void Delete(
            CandidateEducation candidateEducation)
        {
            _dbContext.CandidateEducations
                .Remove(candidateEducation);
        }
    }
}
