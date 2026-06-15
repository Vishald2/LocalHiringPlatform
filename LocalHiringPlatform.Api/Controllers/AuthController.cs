using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Exceptions;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register-candidate")]
    public async Task<IActionResult>
        RegisterCandidate(
            RegisterCandidateRequest request)
    {
        try
        {
            var model =
                new RegisterCandidateModel
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    MobileNumber =
                        request.MobileNumber,
                    Password = request.Password,
                    Role=request.Role,
                    
                };

            await _authService
                .RegisterCandidateAsync(model);

            return Ok(new
            {
                Message =
                    "Candidate registered successfully"
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
                EmailOrMobile =
                    request.EmailOrMobile,

                Password =
                    request.Password
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
    public async Task<IActionResult>
    VerifyEmail(string token)
    {
        await _authService
            .VerifyEmailAsync(token);

        return Ok(
            "Email verified successfully");
    }
}