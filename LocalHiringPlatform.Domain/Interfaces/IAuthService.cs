namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface IAuthService
    {
        Task RegisterCandidateAsync(
            string fullName,
            string email,
            string mobileNumber,
            string password);
    }
}