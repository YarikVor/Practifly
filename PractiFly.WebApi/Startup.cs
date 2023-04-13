using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Practifly.FakerGenerator;
using PractiFly.DateJsonConverter;
using PractiFly.DbContextUtility.Context.Courses;
using PractiFly.DbContextUtility.Context.Materials;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbContextUtility.Context.Users;
using PractiFly.DbContextUtility.Context;
using PractiFly.DbEntities.Users;
using PractiFly.FakerManager;
using PractiFly.WebApi.Services.AuthenticationOptions;
using PractiFly.WebApi.Services.TokenGenerator;
using ErrorContext = PractiFly.DbContextUtility.Context.ErrorContext;
using Microsoft.OpenApi.Models;

namespace PractiFly.WebApi;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services
            .AddSingleton<IAuthOptions, AuthOptions>()
            .AddSingleton<ITokenGenerator, TokenGenerator>();


        services.AddSingleton<IFakerManager, PractiFlyFakerManager>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        AddSwagerGenerationTokenForUser(services);

        AddAuthorizationAndAuthentication(services);

        AddJsonSerializerOptions(services);

        ConfigureDatabase(services);

        InitTables(services);

    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) UseSwagger(app);

        //UseExceptionHandler(app, services);
        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseCertificateForwarding();

    }

    private static void AddSwagerGenerationTokenForUser(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "PractiFly", Version = "v1" });
            c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. " +
                        "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                        "\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
            ;
        });
    }
    private static void AddAuthorizationAndAuthentication(IServiceCollection services)
    {
        var authOptions = services.BuildServiceProvider().GetService<IAuthOptions>() 
            ?? throw new NullReferenceException("IAuthOptions is not found") ;

        services.AddAuthorization();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = authOptions.SymmetricSecurityKey,
                    ValidateIssuerSigningKey = true
                };
            });
    }


    private static void InitTables(IServiceCollection services)
    {
        var context = services.BuildServiceProvider().GetService<IPractiflyContext>()!;
    }

    private static void ConfigureDatabase(IServiceCollection services)
    {
        var config = services.BuildServiceProvider().GetService<IConfiguration>() ?? 
            throw new NullReferenceException("IConfiguration is not found"); 

        var connectionString = config.GetConnectionString("DefaultConnection");
        
        AddPractiFlyDb(services, connectionString);

        var errorConnectionString = config.GetConnectionString("ErrorConnection");

        AddErrorDb(services, errorConnectionString);
    }

    private static void AddErrorDb(IServiceCollection services, string? errorConnectionString)
    {
        services.AddDbContext<ErrorContext>(options =>
            options.UseNpgsql(errorConnectionString));
    }

    private static void AddPractiFlyDb(IServiceCollection services, string? connectionString)
    {
        services
            .AddDbContext<IUsersContext, UsersContext>(
                options => options.UseNpgsql(connectionString))
            .AddDbContext<IMaterialsContext, MaterialsContext>(
                options => options.UseNpgsql(connectionString))
            .AddDbContext<ICoursesContext, CoursesContext>(
                options => options.UseNpgsql(connectionString))
            .AddDbContext<IPractiflyContext, PractiflyContext>(
                options => options.UseNpgsql(connectionString));

        var context = services.BuildServiceProvider().GetService<IPractiflyContext>() as DbContext;
        
        context?.GenerateTestDataIfEmpty();

        services
            .AddIdentity<ApplicationUser, Role>()
            .AddEntityFrameworkStores<UsersContext>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<ApplicationUser>>();
    }


    private static void UseExceptionHandler(WebApplication app, IServiceCollection services)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var errorContext = services.BuildServiceProvider().GetService<ErrorContext>()!;

                await errorContext.ErrorEntities.AddAsync(new ErrorEntity
                {
                    ErrorType = ErrorType.WebApi,
                    Message = context.Features.Get<IExceptionHandlerFeature>()?.Error.Message ?? "Unknown",
                    StackTrace = context.Features.Get<IExceptionHandlerFeature>()?.Error.StackTrace ?? "Unknown",
                    ExceptionName = context.Features.Get<IExceptionHandlerFeature>()?.Error.GetType().Name ?? "Unknown",
                    GeneratedAt = DateTime.UtcNow
                });

                await errorContext.SaveChangesAsync();
            });
        });
    }

    private static void UseSwagger(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    private static void AddJsonSerializerOptions(IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(
            options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            }
        );
    }
}



