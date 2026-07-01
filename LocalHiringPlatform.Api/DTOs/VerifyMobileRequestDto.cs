namespace LocalHiringPlatform.Api.DTOs
{
    public class VerifyMobileRequestDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;
    }
}
