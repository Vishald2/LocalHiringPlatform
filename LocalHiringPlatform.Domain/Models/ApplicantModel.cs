public class ApplicantModel
{
    public Guid JobApplicationId  { get; set; }
    public Guid CandidateProfileId { get; set; }

    public string CandidateName { get; set; }  = string.Empty;

    public string Email { get; set; }  = string.Empty;

    public string MobileNumber { get; set; }
        = string.Empty;

    public DateTime AppliedOn { get; set; }

    public string Status { get; set; }
        = string.Empty;

    public Guid JobId { get; set; } = Guid.Empty;
    public string JobTitle { get; set; } = string.Empty;
}