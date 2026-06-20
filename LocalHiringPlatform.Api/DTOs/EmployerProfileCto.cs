namespace LocalHiringPlatform.Api.DTOs
{
    public class EmployerProfileCto
    {
        public Guid UserId { get; set; }

        public string? CompanyName { get; set; }

        public string? Industry { get; set; }

        public string? Website { get; set; }

        public string? CompanyDescription { get; set; }

        public bool IsEmailVerified { get; set; } = false;
    }
}
