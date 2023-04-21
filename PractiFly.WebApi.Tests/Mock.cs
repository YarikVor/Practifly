using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PractiFly.DbContextUtility.Context.Courses;
using PractiFly.DbContextUtility.Context.Materials;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbContextUtility.Context.Users;
using PractiFly.WebApi.AutoMappers;
using PractiFly.WebApi.Controllers;

namespace PractiFly.WebApi.Tests;

public class Mock
{
    private const string ConnectionString = "User ID=YarikVor;" +
                                            "password=uPGpfLbjt9Z4;" +
                                            "Database=testdb;" +
                                            "host=ep-tight-moon-762347.eu-central-1.aws.neon.tech;";

    private static readonly IServiceProvider ServiceProvider = MakeServiceProvider();


    private static IServiceProvider MakeServiceProvider()
    {
        IServiceCollection services = new ServiceCollection();


        services
            .AddDbContext<IUsersContext, UsersContext>(
                options => NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql(options, ConnectionString))
            .AddDbContext<IMaterialsContext, MaterialsContext>(
                options => NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql(options, ConnectionString))
            .AddDbContext<ICoursesContext, CoursesContext>(
                options => NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql(options, ConnectionString))
            .AddDbContext<IPractiflyContext, PractiFlyContext>(options =>
                NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql(options, ConnectionString));

        services
            .AddScoped<CourseController>()
            .AddScoped<UsersContext>();

        
        services.AddScoped<PractiFlyProfile>();

        services.AddScoped<IMapper, Mapper>(
            e => new Mapper(
                new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(e.GetRequiredService<PractiFlyProfile>());
                    }
                )
            )
        );
        
        return services.BuildServiceProvider();
    }

    public static TEntity Get<TEntity>() where TEntity : class
    {
        return ServiceProvider.GetRequiredService<TEntity>();
    }
}