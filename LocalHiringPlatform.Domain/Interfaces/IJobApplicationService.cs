using LocalHiringPlatform.Domain.Models;

namespace LocalHiringPlatform.Domain.Interfaces;

public interface IJobApplicationService
{
    Task ApplyToJobAsync(ApplyToJobModel model, Guid userId);

    Task<List<MyApplicationModel>>
    GetMyApplicationsAsync(Guid userId);
    Task<List<ApplicantModel>>GetApplicantsAsync(Guid jobId, Guid userId);

    Task<List<ApplicantModel>> GetAllApplicantsByEmployerProfile(Guid userId);
}