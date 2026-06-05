namespace LocalHiringPlatform.Api.DTOs;
public class LoginRequest
{
    public string EmailOrMobile { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}