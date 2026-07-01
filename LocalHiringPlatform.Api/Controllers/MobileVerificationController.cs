using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalHiringPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MobileVerificationController : ControllerBase
{
    private readonly IMobileVerificationService
        _mobileVerificationService;

    public MobileVerificationController(
        IMobileVerificationService mobileVerificationService)
    {
        _mobileVerificationService =
            mobileVerificationService;
    }

    [HttpPost("verify")]
    public async Task<IActionResult> Verify(VerifyMobileRequestDto request)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(
                ClaimTypes.NameIdentifier)!);

        await _mobileVerificationService
            .VerifyMobileAsync(
                userId,
                request.AccessToken);

        return Ok();
    }
}