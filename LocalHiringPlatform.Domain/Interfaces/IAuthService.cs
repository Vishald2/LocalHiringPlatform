using LocalHiringPlatform.Domain.Models;

namespace LocalHiringPlatform.Domain.Interfaces;

public interface IAuthService
{
    Task RegisterCandidateAsync(RegisterCandidateModel model);
    Task<LoginResponseModel> LoginAsync(LoginModel model);
    Task VerifyEmailAsync(string token);

    Task ChangePasswordAsync(Guid userId, ChangePasswordModel model);
}