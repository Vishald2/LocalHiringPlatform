using LocalHiringPlatform.Domain.Entities;

namespace LocalHiringPlatform.Domain.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}