using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class CandidateSearchService
        : ICandidateSearchService
    {
        private readonly
            ICandidateProfileRepository
            _candidateProfileRepository;

        public CandidateSearchService(
            ICandidateProfileRepository
                candidateProfileRepository)
        {
            _candidateProfileRepository =
                candidateProfileRepository;
        }

        public async Task<
            List<CandidateSearchResultModel>>
            SearchAsync(
                CandidateSearchModel model)
        {
            var candidates =
                await _candidateProfileRepository
                    .SearchAsync(
                        model.Name,
                        model.City,
                        model.SkillId);

            return candidates
                .Select(x =>
                    new CandidateSearchResultModel
                    {
                        CandidateProfileId =
                            x.EntityId,

                        FullName =
                            x.FullName,

                        Email =
                            x.User.Email,

                        MobileNumber =
                            x.User.MobileNumber,

                        City =
                            x.City,

                        TotalExperienceYears =
                            x.TotalExperienceYears,

                        ResumeFileName =
                            x.ResumeFileName,

                        ResumeFilePath =
                            x.ResumeFilePath,

                        IsOpenToWork =
                            x.IsOpenToWork
                    })
                .ToList();
        }
    }
}