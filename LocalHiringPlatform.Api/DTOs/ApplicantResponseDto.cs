public class ApplicantResponseDto
{
    public Guid CandidateProfileId { get; set; }

    public string CandidateName { get; set; }
        = string.Empty;

    public string Email { get; set; }
        = string.Empty;

    public string MobileNumber { get; set; }
        = string.Empty;

    public DateTime AppliedOn { get; set; }

    public string Status { get; set; }
        = string.Empty;
}