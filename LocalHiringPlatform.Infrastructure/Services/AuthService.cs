using LocalHiringPlatform.Domain.Configuration;
using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Enums;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.Infrastructure.EmailTemplates;
using LocalHiringPlatform.ServiceBus.Interfaces;
using Microsoft.Extensions.Options;

namespace LocalHiringPlatform.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ICandidateProfileRepository _candidateProfileRepository;
    private readonly IEmployerProfileRepository _employerProfileRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IJwtTokenService _jwtTokenService;
    private readonly IEmailService _emailService;
    private readonly IOptions<ApplicationSettings> _applicationSettings;
    private readonly IServiceBusPublisher _serviceBusPublisher;

    public AuthService(IUserRepository userRepository,
        ICandidateProfileRepository candidateProfileRepository,
        IEmployerProfileRepository employerProfileRepository,
        IUnitOfWork unitOfWork,
        IJwtTokenService jwtTokenService,
        IEmailService emailService,
        IOptions<ApplicationSettings> applicationSettings,
        IServiceBusPublisher serviceBusPublisher)
    {
        _userRepository = userRepository;
        _candidateProfileRepository = candidateProfileRepository;
        _employerProfileRepository = employerProfileRepository;
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
        _emailService = emailService;
        _applicationSettings = applicationSettings;
        _serviceBusPublisher = serviceBusPublisher;
    }

    public async Task RegisterCandidateAsync(RegisterCandidateModel model)
    {
        var existingEmail = await _userRepository.GetByEmailAsync(model.Email);

        if (existingEmail != null)
        {
            throw new BusinessException("Email already exists");
        }

        var existingMobile = await _userRepository.GetByMobileAsync(model.MobileNumber);

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
            EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24)
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

        /*SEND VERIIFCATION MAIL*/


        var verificationUrl =
    $"{_applicationSettings.Value.FrontendUrl}/verify-email?token={user.EmailVerificationToken}";
        EmailRequestModel emailRequestModel = new EmailRequestModel() { 
        To = model.Email,
        Subject="LocalHire AI Verification Email",
        HtmlBody= VerificationEmailTemplate.Build(
            verificationUrl)

        };

        /*PUBLISHING MESSAGE TO AZURE MESSAGE QUEUE*/
        await _serviceBusPublisher.PublishAsync(emailRequestModel);

    }

    public async Task<LoginResponseModel> LoginAsync(LoginModel model)
    {
        var user = await _userRepository
                .GetByEmailOrMobileAsync(model.EmailOrMobile);

        if (user == null)
        {
            throw new BusinessException(
                "Invalid credentials");
        }

        bool passwordValid = BCrypt.Net.BCrypt.Verify(
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
            throw new BusinessException("Invalid token");
        }
         
        user.EmailVerified = true;

        user.EmailVerifiedOn = DateTime.UtcNow;

        user.EmailVerificationToken = null;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task ChangePasswordAsync(
    Guid userId,
    ChangePasswordModel model)
    {
        var user =
            await _userRepository
                .GetByIdAsync(userId);

        if (user == null)
        {
            throw new BusinessException(
                "User not found.");
        }

        if (!BCrypt.Net.BCrypt.Verify(
                model.CurrentPassword,
                user.PasswordHash))
        {
            throw new BusinessException(
                "Current password is incorrect.");
        }

        if (model.NewPassword !=
            model.ConfirmPassword)
        {
            throw new BusinessException(
                "New password and confirm password do not match.");
        }

        if (BCrypt.Net.BCrypt.Verify(
                model.NewPassword,
                user.PasswordHash))
        {
            throw new BusinessException(
                "New password must be different from the current password.");
        }

        user.PasswordHash =
            BCrypt.Net.BCrypt.HashPassword(
                model.NewPassword);

        await _unitOfWork
            .SaveChangesAsync();
    }
}