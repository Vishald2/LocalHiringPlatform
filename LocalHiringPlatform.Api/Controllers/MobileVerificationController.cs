using LocalHiringPlatform.Api.DTOs;
using LocalHiringPlatform.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace LocalHiringPlatform.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MobileVerificationController
    : ControllerBase
{
    private readonly
        IMobileVerificationService
        _mobileVerificationService;

    public MobileVerificationController(
        IMobileVerificationService
            mobileVerificationService)
    {
        _mobileVerificationService =
            mobileVerificationService;
    }

    [HttpPost("send")]
    public async Task<IActionResult>
        SendOtp()
    {
        Guid userId =
            Guid.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)!
                .Value);

      string otp=  await _mobileVerificationService
            .SendOtpAsync(
                userId);


        return Ok(new
        {
            Message = "OTP sent successfully.",
            Otp = otp
        });
    }

    [HttpPost("verify")]
    public async Task<IActionResult>
        VerifyOtp(
            VerifyMobileOtpRequestDto request)
    {
        Guid userId =
            Guid.Parse(
                User.FindFirst(
                    ClaimTypes.NameIdentifier)!
                .Value);

        await _mobileVerificationService
            .VerifyOtpAsync(
                userId,
                request.Otp);

        return Ok(
            new
            {
                Message =
                    "Mobile verified successfully."
            });
    }
}