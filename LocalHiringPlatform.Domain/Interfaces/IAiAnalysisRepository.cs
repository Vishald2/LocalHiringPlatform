using LocalHiringPlatform.Domain.Entities;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface IAiAnalysisRepository
    {
        Task<AiAnalysis?>
            GetByJobApplicationIdAsync(
                Guid jobApplicationId);

        Task AddAsync(
            AiAnalysis aiAnalysis);

        void Update(
            AiAnalysis aiAnalysis);
    }
}
