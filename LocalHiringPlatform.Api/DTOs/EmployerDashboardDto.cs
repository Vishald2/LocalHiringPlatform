namespace LocalHiringPlatform.Api.DTOs
{
    public class EmployerDashboardDto
    {
        public int TotalJobs { get; set; }
        public int ActiveJobs { get; set; }
        public int TotalApplicants { get; set; }
        public int NewApplications { get; set; }
    }
}
