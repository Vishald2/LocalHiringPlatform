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
                    Password = request.Password
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
}