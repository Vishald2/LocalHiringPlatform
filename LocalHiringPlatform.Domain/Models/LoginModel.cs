namespace LocalHiringPlatform.Domain.Models;

public class LoginModel
{
    public string EmailOrMobile { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}