namespace LocalHiringPlatform.Api.DTOs
{
    public class RegisterCandidateRequest
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string Password { get; set; }
    }
}
