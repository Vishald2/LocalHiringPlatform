using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalHiringPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService,
            IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new BusinessException(
                    "Password is required.");
            }

            if (password.Length < 8)
            {
                throw new BusinessException(
                    "Password must be at least 8 characters.");
            }

            if (!password.Any(char.IsUpper))
            {
                throw new BusinessException(
                    "Password must contain at least one uppercase letter.");
            }

            if (!password.Any(char.IsLower))
            {
                throw new BusinessException(
                    "Password must contain at least one lowercase letter.");
            }

            if (!password.Any(char.IsDigit))
            {
                throw new BusinessException(
                    "Password must contain at least one number.");
            }
        }

        [HttpPost("register-candidate")]
        public async Task<IActionResult> RegisterCandidate(RegisterCandidateRequest request)
        {
            try
            {
                var model =
                    new RegisterCandidateModel
                    {
                        FullName = request.FullName,
                        Email = request.Email,
                        MobileNumber = request.MobileNumber,
                        Password = request.Password,
                        Role = request.Role,
                    };

                ValidatePassword(request.Password);

                await _authService.RegisterCandidateAsync(model);

                return Ok(new
                {
                    Message = "Candidate registered successfully"
                });
            }
            catch (BusinessException ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult>
        Login(LoginRequest request)
        {
            try
            {
                var model = new LoginModel
                {
                    EmailOrMobile = request.EmailOrMobile,

                    Password = request.Password
                };

                var response = await _authService.LoginAsync(model);

                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            await _authService.VerifyEmailAsync(token);
            return Ok(new VerifyEmailResponseModel
            {
                Success = true,
                Message = "Email verified successfully."
            });
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(
        ChangePasswordModel model)
        {
            var userId =
                Guid.Parse(
                    User.FindFirst(
                        ClaimTypes.NameIdentifier)!
                    .Value);

            ValidatePassword(model.NewPassword);

            await _authService
                .ChangePasswordAsync(
                    userId,
                    model);

            return Ok(
                new
                {
                    message =
                        "Password changed successfully."
                });
        }
    }
}