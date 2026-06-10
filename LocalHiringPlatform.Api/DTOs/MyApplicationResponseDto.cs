namespace LocalHiringPlatform.Api.DTOs
{
    public class MyApplicationResponseDto
    {
        public Guid JobId { get; set; }

        public string JobTitle { get; set; }
            = string.Empty;

        public DateTime AppliedOn { get; set; }

        public string Status { get; set; }
            = string.Empty;
    }
}
