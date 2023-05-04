namespace PractiFly.DbContextUtility.Abstraction;

public interface IBasicDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}