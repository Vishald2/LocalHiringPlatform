namespace LocalHiringPlatform.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}