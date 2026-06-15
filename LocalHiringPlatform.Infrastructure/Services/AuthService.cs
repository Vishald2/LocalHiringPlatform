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
    private readonly IEmployerProfileRepository _employerProfileRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        IUserRepository userRepository,
        ICandidateProfileRepository candidateProfileRepository,
        IEmployerProfileRepository employerProfileRepository,
        IUnitOfWork unitOfWork,
        IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _candidateProfileRepository = candidateProfileRepository;
        _employerProfileRepository = employerProfileRepository;
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
    }

    public async Task RegisterCandidateAsync(RegisterCandidateModel model)
    {
        var existingEmail =
            await _userRepository.GetByEmailAsync(model.Email);

        if (existingEmail != null)
        {
            throw new BusinessException("Email already exists");
        }

        var existingMobile =
            await _userRepository.GetByMobileAsync(model.MobileNumber);

        if (existingMobile != null)
        {
            throw new BusinessException("Mobile number already exists");
        }

        var user = new User
        {
            Email = model.Email,
            MobileNumber = model.MobileNumber,
            PasswordHash =  BCrypt.Net.BCrypt.HashPassword(model.Password),
            Role = model.Role,
            EmailVerified = false,
            EmailVerificationToken = Guid.NewGuid().ToString(),
        };

        await _userRepository.AddAsync(user);

        if (user.Role == UserRole.Candidate)
        {
            var profile = new CandidateProfile
            {
                UserId = user.EntityId,
                FullName = model.FullName
            };

            await _candidateProfileRepository.AddAsync(profile);
        }
        else if (user.Role == UserRole.Employer)
        {
            var profile = new EmployerProfile
            {
                UserId = user.EntityId
            };

            await _employerProfileRepository.AddAsync(profile);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<LoginResponseModel> LoginAsync(
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

        return new LoginResponseModel
        {
            Token = _jwtTokenService.GenerateToken(user),

            Role = user.Role.ToString()
        };
    }

    public async Task VerifyEmailAsync(string token)
    {
        var user = await _userRepository
                .GetByEmailVerificationTokenAsync(token);

        if (user == null)
        {
            throw new BusinessException(
                "Invalid token");
        }

        user.EmailVerified = true;

        user.EmailVerifiedOn = DateTime.UtcNow;

        user.EmailVerificationToken = null;

        await _unitOfWork.SaveChangesAsync();
    }
}