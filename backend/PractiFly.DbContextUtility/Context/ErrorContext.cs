using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbContextUtility.Context;

public class ErrorContext : DbContext
{
    public DbSet<ErrorEntity> ErrorEntities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ErrorEntity>().HasData(
            new ErrorEntity
            {
                Id = 1,
                ErrorType = ErrorType.Unknown,
                Message = "Unknown error",
                StackTrace = "Unknown error",
                ExceptionName = "Unknown error",
                GeneratedAt = DateTime.Now
            }
        );
    }
}

public class ErrorEntity
{
    public int Id { get; set; }
    public ErrorType ErrorType { get; set; }
    public string Message { get; set; } = null!;
    public string StackTrace { get; set; } = null!;
    public string ExceptionName { get; set; } = null!;
    public DateTime GeneratedAt { get; set; }
}

public enum ErrorType
{
    Unknown,
    Database,
    WebApi
}