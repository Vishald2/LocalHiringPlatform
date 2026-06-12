namespace LocalHiringPlatform.Api.DTOs
{
    public class UpdateApplicationStatusRequestDto
    {
        public Guid JobApplicationId
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        } = string.Empty;
    }
}
