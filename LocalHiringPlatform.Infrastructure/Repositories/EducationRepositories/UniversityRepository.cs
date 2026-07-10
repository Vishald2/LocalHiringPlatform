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
    public class UniversityRepository : IUniversityRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UniversityRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<University>> GetAllAsync()
        {
            return await _dbContext.Universities
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<University?> GetByIdAsync(int universityId)
        {
            return await _dbContext.Universities
                .FirstOrDefaultAsync(x => x.UniversityId == universityId);
        }

        public async Task<List<University>> SearchAsync(string searchText)
        {
            searchText = searchText.Trim();

            return await _dbContext.Universities
                .Where(x => x.IsActive &&
                    (x.Name.Contains(searchText) ||
                     x.City.Contains(searchText) ||
                     x.State.Contains(searchText)))
                .OrderBy(x => x.Name)
                .Take(20)
                .ToListAsync();
        }
    }
}
