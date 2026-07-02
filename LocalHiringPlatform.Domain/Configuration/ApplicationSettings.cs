namespace LocalHiringPlatform.Domain.Configuration
{
    public class ApplicationSettings
    {
        public string FrontendUrl { get; set; } = string.Empty;
        public string PromptFolder { get; set; } = string.Empty;

        public bool UseRedis { get; set; } = false;
    }
}
