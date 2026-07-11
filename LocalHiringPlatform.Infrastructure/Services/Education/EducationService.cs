using LocalHiringPlatform.Domain.Interfaces.CandidateEducationInterfaces;
using LocalHiringPlatform.Domain.Models.Education;

public class EducationService : IEducationService
{
    private readonly IEducationRepository _educationRepository;

    public EducationService(
        IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }

    public async Task<List<EducationModel>> GetAllAsync()
    {
        var educations =
            await _educationRepository.GetAllAsync();

        return educations
            .Select(x => new EducationModel
            {
                EducationId = x.EducationId,
                EducationName = x.Name,
                Code = x.Code
            })
            .ToList();
    }
}