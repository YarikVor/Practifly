using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.WebApi.Context;

namespace PractiFly.WebApi;

public abstract class DesignTimeDbContentFactory<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
    where TDbContext : DbContext
{
    public TDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();

        optionsBuilder.UseNpgsql(
            "User ID=YarikVor;password=uPGpfLbjt9Z4;Database=testmigration;host=ep-tight-moon-762347.eu-central-1.aws.neon.tech");

        return (TDbContext) Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options)!;
    }
}

public class PractiFlyContextFactory : DesignTimeDbContentFactory<PractiFlyContext>
{
}

public class UserIdentityContextFactory : DesignTimeDbContentFactory<UserIdentityDbContext>
{
    
}