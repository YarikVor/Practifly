using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.Context;

namespace PractiFly.Tests.EntityFromDb;

public static class Mock
{

    public const string ConnectionString
        = "User ID=YarikVor;" +
          "password=uPGpfLbjt9Z4;" +
          "Database=testdb;" +
          "host=ep-tight-moon-762347.eu-central-1.aws.neon.tech";

    
    private static DbContextOptions<TContext> CreateOptions<TContext>()
        where TContext : DbContext
    {
        return new DbContextOptionsBuilder<TContext>()
            .UseNpgsql(ConnectionString)
            .Options;
    } 

    public static UsersContext CreateUsersContext()
    {
        var options = CreateOptions<UsersContext>();
        return new UsersContext(options);
    }
    public static MaterialsContext CreateMaterialsContext()
    {
        var options = CreateOptions<MaterialsContext>();
        return new MaterialsContext(options);
    }
    public static CoursesContext CreateCoursesContext()
    {
        var options = CreateOptions<CoursesContext>();
        return new CoursesContext(options);
    }
}