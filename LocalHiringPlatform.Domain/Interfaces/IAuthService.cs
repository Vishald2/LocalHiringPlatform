using LocalHiringPlatform.Domain.Models;

namespace LocalHiringPlatform.Domain.Interfaces;

public interface IAuthService
{
    Task RegisterCandidateAsync(
        RegisterCandidateModel model);
    Task<string> LoginAsync(
     LoginModel model);
}