namespace LocalHiringPlatform.Api.DTOs
{
    public class EmployerDashboardResponseDto
    {
        public int TotalJobs { get; set; }

        public int ActiveJobs { get; set; }

        public int TotalApplicants { get; set; }

        public int NewApplications { get; set; }
    }
}
