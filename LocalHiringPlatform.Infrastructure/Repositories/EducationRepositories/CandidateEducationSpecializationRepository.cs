using LocalHiringPlatform.Domain.Entities.CandidateEducationEntities;
using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<CandidateCourseSpecialization>>
            GetByCandidateEducationEntityIdAsync(
                Guid candidateEducationEntityId)
        {
            return await _dbContext.CandidateEducationSpecialization
              //  .Where(x => x.CandidateEducationEntityId == candidateEducationEntityId)
              //  .Include(x => x.CourseSpecialization)
             //       .ThenInclude(x => x.Specialization)
                .ToListAsync();
        }

        public async Task AddRangeAsync(
            IEnumerable<CandidateCourseSpecialization> specializations)
        {
            await _dbContext.CandidateEducationSpecialization
                .AddRangeAsync(specializations);
        }

        public void RemoveRange(
            IEnumerable<CandidateCourseSpecialization> specializations)
        {
            _dbContext.CandidateEducationSpecialization
                .RemoveRange(specializations);
        }
    }
}
