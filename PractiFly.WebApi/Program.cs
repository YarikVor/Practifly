using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.Attributes;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Db.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

ConfigureServices(services);

services.AddControllers().AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCertificateForwarding();

app.Run();

//Hi! :)
void ConfigureServices(IServiceCollection services)
{
    // Other DI initializations

    var config = services.BuildServiceProvider().GetService<IConfiguration>();

    var connectionString = config.GetConnectionString("DefaultConnection");
    
    
    services.AddDbContext<IUsersContext, UsersContext>(options =>
            options.UseNpgsql(connectionString));
    services.AddDbContext<IMaterialsContext, MaterialsContext>(options =>
        options.UseNpgsql(connectionString));
    services.AddDbContext<ICoursesContext, CoursesContext>(options =>
        options.UseNpgsql(connectionString));
    services.AddDbContext<IPractiflyContext, PractiflyContext>(options =>
        options.UseNpgsql(connectionString));
    
}

