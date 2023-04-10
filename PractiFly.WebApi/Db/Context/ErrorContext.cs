using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.Db.Context;

public class ErrorContext : DbContext
{
    public ErrorContext(DbContextOptions<ErrorContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<ErrorEntity> ErrorEntities { get; set; } = null!;
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