using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Models.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services.Education
{
    public class UniversityService : IUniversityService
    {
        private readonly IUniversityRepository
            _universityRepository;

        public UniversityService(
            IUniversityRepository universityRepository)
        {
            _universityRepository =
                universityRepository;
        }

        public async Task<List<UniversityResponseModel>>
            GetAllAsync()
        {
            var universities =
                await _universityRepository.GetAllAsync();

            return universities
                .Select(x => new UniversityResponseModel
                {
                    UniversityId = x.UniversityId,
                    Name = x.Name,
                    City = x.City,
                    State = x.State
                })
                .ToList();
        }

        public async Task<List<UniversityResponseModel>>
            SearchAsync(
                string searchText)
        {
            var universities =
                await _universityRepository.SearchAsync(
                    searchText);

            return universities
                .Select(x => new UniversityResponseModel
                {
                    UniversityId = x.UniversityId,
                    Name = x.Name,
                    City = x.City,
                    State = x.State
                })
                .ToList();
        }
    }
}
