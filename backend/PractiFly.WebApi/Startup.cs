using System.ComponentModel;
using System.Reflection;
using Amazon.S3;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PractiFly.DateJsonConverter;
using PractiFly.DateJsonConverter.Schemas;
using PractiFly.DbContextUtility.Context;
using PractiFly.DbContextUtility.Context.Courses;
using PractiFly.DbContextUtility.Context.Materials;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbContextUtility.Context.Users;
using PractiFly.DbEntities.Users;
using Practifly.FakerGenerator;
using PractiFly.FakerManager;
using PractiFly.WebApi.AutoMappers;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Services.AuthenticationOptions;
using PractiFly.WebApi.Services.TokenGenerator;
using ErrorContext = PractiFly.DbContextUtility.Context.ErrorContext;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi;

public class Startup
{
    #region Fields

    private readonly IConfiguration _configuration;

    #endregion

    #region Constructors

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration
                         ?? throw new NullReferenceException("IConfiguration is not found");
    }

    #endregion

    #region Configure Services

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("_MyPolicy",
                policy =>
                {
                    policy.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services
            .AddSingleton<IAuthConfiguration, AuthConfiguration>()
            .AddSingleton<ITokenGenerator, TokenGenerator>();

        services.AddSingleton<IFakerManager, PractiFlyFakerManager>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        AddSwaggerGenerationTokenForUser(services);

        AddAuthorizationAndAuthentication(services);

        AddJsonSerializerOptions(services);

        ConfigureDatabase(services);

        AddIdentityUserAndRole(services);

        InitTables(services);

        services.AddScoped<IConfigurationProvider, PractiFlyMapperConfiguration>();
        services.AddScoped<IMapper, PractiFlyMapper>();

        services.AddScoped<IAmazonS3, PractiFlyAmazonS3Client>();
        services.AddSingleton<IAuthConfiguration, AuthConfiguration>();
        services.AddSingleton<IBucketConfiguration, BucketConfiguration>();
        services.AddScoped<IAmazonS3ClientManager, PractiFlyAmazonS3ClientManager>();
    }

    #endregion

    #region Configure Application

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //Add support DateTime, DateOnly and TimeOnly where UTC = +00:00
        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);
        //AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        //if (env.IsDevelopment())
        UseSwagger(app);

        //UseExceptionHandler(app, services);
        app.UseRouting();

        app.UseCors("_MyPolicy");
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseCertificateForwarding();
    }

    #endregion

    #region Authorization and Authentication

    private static void AddAuthorizationAndAuthentication(IServiceCollection services)
    {
        var authOptions = services.BuildServiceProvider().GetService<IAuthConfiguration>()
                          ?? throw new NullReferenceException("IAuthOptions is not found");

        services.AddAuthorization();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options =>
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
                }
            );
    }

    #endregion

    #region Add Identity User And Role

    private static void AddIdentityUserAndRole(IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<UserIdentityDbContext>()
            .AddDefaultTokenProviders()
            .AddUserManager<UserManager<User>>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleManager<RoleManager<Role>>();


        services.Configure<IdentityOptions>(options =>
        {
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        });
    }

    #endregion

    #region Add Exception Handler

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

    #endregion

    #region Json Serializer Options

    private static void AddJsonSerializerOptions(IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(
            options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            }
        );

        TypeDescriptor.AddAttributes(typeof(DateOnly), new TypeConverterAttribute(typeof(DateOnlyTypeConverter)));
    }

    #endregion

    #region Configure Swagger

    private static void AddSwaggerGenerationTokenForUser(IServiceCollection services)
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
                        "\r\n\r\nExample: \"Bearer [token]\"",
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

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            c.MapType<DateOnly>(DateOnlyApiSchema.Create);
        });
    }

    private static void UseSwagger(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    #endregion

    #region Configure and Init Database

    private void ConfigureDatabase(IServiceCollection services)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        AddPractiFlyDb(services, connectionString);

        var errorConnectionString = _configuration.GetConnectionString("ErrorConnection");
        //AddErrorDb(services, errorConnectionString);
    }

    private static void AddErrorDb(IServiceCollection services, string? errorConnectionString)
    {
        services.AddDbContext<ErrorContext>(options =>
            options.UseNpgsql(errorConnectionString));
    }

    private static void AddPractiFlyDb(IServiceCollection services, string? connectionString)
    {
        services
            .AddDbContext<IUsersContext, UsersContext>(SetConnectionString)
            .AddDbContext<IMaterialsContext, MaterialsContext>(SetConnectionString)
            .AddDbContext<ICoursesContext, CoursesContext>(SetConnectionString)
            .AddDbContext<IPractiflyContext, PractiFlyContext>(SetConnectionString)
            .AddDbContext<UserIdentityDbContext>(SetConnectionString);

        void SetConnectionString(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(connectionString);
        }
    }

    private static void InitTables(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var identityContext = serviceProvider.GetService<UserIdentityDbContext>();
        var practiFlyContext = serviceProvider.GetService<IPractiflyContext>() as DbContext;
        identityContext!.Database.Migrate();

        GenerateRules(identityContext);

        practiFlyContext!.Database.Migrate();

        //practiFlyContext.GenerateTestDataIfEmpty();
    }

    private static void GenerateRules(UserIdentityDbContext identityContext)
    {
        if (!identityContext.Roles.Any())
        {
            identityContext.Roles.AddRange(GenerateRole("user"), GenerateRole("admin"), GenerateRole("teacher"),
                GenerateRole("manager"));

            identityContext.SaveChanges();
        }

        Role GenerateRole(string name)
        {
            return new Role
            {
                Name = name,
                NormalizedName = name.ToUpper()
            };
        }
    }

    #endregion
}