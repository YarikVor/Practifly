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

        // Db

        services
            .AddDbContext<IUsersContext, UsersContext>(UseConnectionString)
            .AddDbContext<IMaterialsContext, MaterialsContext>(UseConnectionString)
            .AddDbContext<ICoursesContext, CoursesContext>(UseConnectionString)
            .AddDbContext<IPractiflyContext, PractiFlyContext>(UseConnectionString);

        void UseConnectionString(DbContextOptionsBuilder options) 
            => options.UseNpgsql(ConnectionString);

        // Use controllers
        services
            .AddScoped<CourseController>()
            .AddScoped<UserController>();

        // Use Profiles for AutoMapper
        services.AddScoped<PractiFlyProfile>();

        // Use Mapper
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