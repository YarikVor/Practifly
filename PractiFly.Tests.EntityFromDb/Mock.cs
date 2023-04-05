using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.Tests.EntityFromDb;

public static class Mock
{

    public const string ConnectionString
        = "User ID=YarikVor;" +
          "password=uPGpfLbjt9Z4;" +
          "Database=testdb;" +
          "host=ep-tight-moon-762347.eu-central-1.aws.neon.tech";

    private static Lazy<DbContextOptions<UsersContext>> _lazyUsersContextOption =
        new Lazy<DbContextOptions<UsersContext>>(() =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<UsersContext>();
            return optionsBuilder.UseNpgsql(ConnectionString).Options;
        });

    public static DbContextOptions<UsersContext> UsersContextOption => _lazyUsersContextOption.Value;

    public static UsersContext CreateUsersContext()
    {
        return new UsersContext(UsersContextOption);
    }
}