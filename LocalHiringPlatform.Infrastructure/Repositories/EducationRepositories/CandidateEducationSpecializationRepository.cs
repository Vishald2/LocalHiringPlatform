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
    public class CandidateEducationSpecializationRepository
         : ICandidateEducationSpecializationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CandidateEducationSpecializationRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(
            CandidateEducationSpecialization candidateCourseSpecialization)
        {
            await _dbContext.CandidateEducationSpecializations
                .AddAsync(candidateCourseSpecialization);
        }

        public void Update(
            CandidateEducationSpecialization candidateCourseSpecialization)
        {
            _dbContext.CandidateEducationSpecializations
                .Update(candidateCourseSpecialization);
        }

        public void Delete(
            CandidateEducationSpecialization candidateCourseSpecialization)
        {
            _dbContext.CandidateEducationSpecializations
                .Remove(candidateCourseSpecialization);
        }

        public async Task<List<CandidateEducationSpecialization>>
            GetByCandidateProfileIdAsync(
                Guid candidateProfileId,
                int courseId)
        {
            return null;// await _dbContext.CandidateCourseSpecializations;
                //.Where(x =>
                //    x.ProfileId == candidateProfileId &&
                //    x.CourseId == courseId)
                //.Include(x => x.Specialization)
                //.OrderBy(x => x.Specialization.Name)
                //.ToListAsync();
        }

        public async Task AddRangeAsync(
            IEnumerable<CandidateEducationSpecialization> candidateEducationSpecializations)
        {
            await _dbContext.CandidateEducationSpecializations
                .AddRangeAsync(candidateEducationSpecializations);
        }

        public void RemoveRange(IEnumerable<CandidateEducationSpecialization> candidateEducationSpecializations)
        {
             _dbContext.CandidateEducationSpecializations
                .RemoveRange(candidateEducationSpecializations);
        }

        public async Task<List<CandidateEducationSpecialization>> GetByCandidateEducationIdAsync(Guid candidateEducationId)
        {
            return await _dbContext.CandidateEducationSpecializations
                .Where(x => x.CandidateEducationEntityId == candidateEducationId)
                .Include(x => x.Specialization)
                .OrderBy(x => x.Specialization.Name)
                .ToListAsync();
        }
    }
}
