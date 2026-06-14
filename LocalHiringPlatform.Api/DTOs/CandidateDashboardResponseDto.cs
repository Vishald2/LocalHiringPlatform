namespace LocalHiringPlatform.Api.DTOs
{
    public class CandidateDashboardResponseDto
    {
        public int TotalApplications { get; set; }

        public int Shortlisted { get; set; }

        public int InterviewScheduled { get; set; }

        public int Rejected { get; set; }

        public int Hired { get; set; }
    }
}
