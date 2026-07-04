namespace LocalHiringPlatform.Domain.Models.AI
{
    public class JobSearchIntentModel
    {
        public string IndustryType { get; set; } = "";

        public List<string> Skills { get; set; } = new();

        public string City { get; set; } = "";

        public int? ExperienceInYears { get; set; }

        public decimal? MinimumSalary { get; set; }

        public bool RemoteOnly { get; set; }

        public string EmploymentType { get; set; } = "";
    }
}