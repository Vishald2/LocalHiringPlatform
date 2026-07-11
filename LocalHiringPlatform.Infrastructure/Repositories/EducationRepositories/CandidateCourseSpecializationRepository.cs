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
    public class CandidateCourseSpecializationRepository
         : ICandidateCourseSpecializationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CandidateCourseSpecializationRepository(
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
    }
}
