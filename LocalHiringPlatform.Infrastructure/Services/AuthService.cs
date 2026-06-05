using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Enums;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;

namespace LocalHiringPlatform.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ICandidateProfileRepository _candidateProfileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        IUserRepository userRepository,
        ICandidateProfileRepository candidateProfileRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _candidateProfileRepository = candidateProfileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterCandidateAsync(
        RegisterCandidateModel model)
    {
        var existingEmail =
            await _userRepository.GetByEmailAsync(
                model.Email);

        if (existingEmail != null)
        {
            throw new BusinessException("Email already exists");
        }

        var existingMobile =
            await _userRepository.GetByMobileAsync(
                model.MobileNumber);

        if (existingMobile != null)
        {
            throw new BusinessException("Mobile number already exists");
        }

        var user = new User
        {
            Email = model.Email,
            MobileNumber = model.MobileNumber,

            PasswordHash =  BCrypt.Net.BCrypt.HashPassword(model.Password),

            Role = UserRole.Candidate
        };

        await _userRepository.AddAsync(user);

        var profile = new CandidateProfile
        {
            UserId = user.EntityId,
            FullName = model.FullName
        };

        await _candidateProfileRepository
            .AddAsync(profile);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task LoginAsync(
    LoginModel model)
    {
        var user =
            await _userRepository
                .GetByEmailOrMobileAsync(
                    model.EmailOrMobile);

        if (user == null)
        {
            throw new BusinessException(
                "Invalid credentials");
        }

        bool passwordValid =
            BCrypt.Net.BCrypt.Verify(
                model.Password,
                user.PasswordHash);

        if (!passwordValid)
        {
            throw new BusinessException(
                "Invalid credentials");
        }
    }
}