using LocalHiringPlatform.Domain.Models;

namespace LocalHiringPlatform.Domain.Interfaces;

public interface IJobApplicationService
{
    Task ApplyToJobAsync(
        ApplyToJobModel model,
        Guid userId);
}