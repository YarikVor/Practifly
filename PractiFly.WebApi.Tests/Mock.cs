using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Controllers;
using PractiFly.WebApi.Db.Context;

namespace PractiFly.WebApi.Tests;

public class Mock
{
    private static readonly IServiceProvider ServiceProvider = MakeServiceProvider();

    private const string ConnectionString = "User ID=YarikVor;" +
                                            "password=uPGpfLbjt9Z4;" +
                                            "Database=testdb;" +
                                            "host=ep-tight-moon-762347.eu-central-1.aws.neon.tech;";
                                      
                                           
    private static IServiceProvider MakeServiceProvider()
    {
        IServiceCollection services = new ServiceCollection();

        services
            .AddDbContext<IUsersContext, UsersContext>(
                options => options.UseNpgsql(ConnectionString))
            .AddDbContext<IMaterialsContext, MaterialsContext>(
                options => options.UseNpgsql(ConnectionString))
            .AddDbContext<ICoursesContext, CoursesContext>(
                options => options.UseNpgsql(ConnectionString))
            .AddDbContext<IPractiflyContext, PractiflyContext>(options =>
                options.UseNpgsql(ConnectionString))
            .AddTransient<UserController>();

        return services.BuildServiceProvider();
    }
    
    public static TEntity Get<TEntity>() where TEntity : class
    {
        return ServiceProvider.GetRequiredService<TEntity>();
    }
}