using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.Courses;
using PractiFly.DbContextUtility.Context.Materials;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbContextUtility.Context.Users;

namespace PractiFly.Tests.EntityFromDb;

public static class Mock
{
    private const string ConnectionString
        = "User ID=YarikVor;" +
          "password=uPGpfLbjt9Z4;" +
          "Database=testdb;" +
          "host=ep-tight-moon-762347.eu-central-1.aws.neon.tech";


    static Mock()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    private static DbContextOptions<TContext> CreateOptions<TContext>()
        where TContext : DbContext
    {
        return new DbContextOptionsBuilder<TContext>()
            .UseNpgsql(ConnectionString)
            .Options;
    }

    public static TContext CreateContext<TContext>() where TContext : DbContext
    {
        var options = CreateOptions<TContext>();
        return (TContext)Activator.CreateInstance(typeof(TContext), options)!;
    }


    public static UsersContext CreateUsersContext()
    {
        return CreateContext<UsersContext>();
    }

    public static MaterialsContext CreateMaterialsContext()
    {
        return CreateContext<MaterialsContext>();
    }

    public static CoursesContext CreateCoursesContext()
    {
        return CreateContext<CoursesContext>();
    }

    public static PractiFlyContext CreatePractiflyContext()
    {
        return CreateContext<PractiFlyContext>();
    }
}