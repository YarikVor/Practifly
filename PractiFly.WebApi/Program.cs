using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PractiFly.WebApi.Attributes;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Db.Context;
using ErrorContext = PractiFly.WebApi.Db.Context.ErrorContext;

namespace PractiFly.WebApi;

internal static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore", Version = "v1" });
            c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            }); ;
        });

        AuthOptions.Init(services.BuildServiceProvider().GetService<IConfiguration>());
        
        AddAuthorizationAndAuthentication(services);

        AddJsonSerializerOptions(services);

        ConfigureDatabase(services);

        InitTables(services);

        var app = builder.Build();

        if (app.Environment.IsDevelopment()) UseSwagger(app);

        //UseExceptionHandler(app, services);

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.UseCertificateForwarding();

        app.Run();
    }

    private static void AddAuthorizationAndAuthentication(IServiceCollection services)
    {
        services.AddAuthorization();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // указывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = AuthOptions.ISSUER,
                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = AuthOptions.AUDIENCE,
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,
                    // установка ключа безопасности
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // валидация ключа безопасности
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
        var config = services.BuildServiceProvider().GetService<IConfiguration>();

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
            .AddDbContext<IPractiflyContext, PractiflyContext>(options =>
                options.UseNpgsql(connectionString));
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

    private static void UseSwagger(WebApplication app)
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

public static class AuthOptions
{
    public static string ISSUER => _section["Issuer"];
    public static string AUDIENCE => _section["Audience"];
    public static string KEY => _section["Key"];

    private static IConfigurationSection _section;

    public static void Init(IConfiguration configuration)
    {
        _section = configuration.GetSection("Secret");
    }

    public static SecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(KEY)
        );
    }
}