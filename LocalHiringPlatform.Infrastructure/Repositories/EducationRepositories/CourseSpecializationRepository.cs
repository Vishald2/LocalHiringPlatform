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
    public class CourseSpecializationRepository
            : ICourseSpecializationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseSpecializationRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CourseSpecialization>> GetByCourseIdAsync(
            int courseId)
        {
            return await _dbContext.CourseSpecializations
                .Where(x => x.CourseId == courseId)
                .Include(x => x.Specialization)
                .OrderBy(x => x.Specialization.DisplayOrder)
                .ThenBy(x => x.Specialization.Name)
                .ToListAsync();
        }

        public async Task<List<CourseSpecialization>> GetByIdsAsync(
            List<int> courseSpecializationIds)
        {
            return await _dbContext.CourseSpecializations
                .Where(x => courseSpecializationIds
                    .Contains(x.CourseSpecializationId))
                .Include(x => x.Specialization)
                .ToListAsync();
        }
    }
}
