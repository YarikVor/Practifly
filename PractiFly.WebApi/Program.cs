using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureServices(builder.Services);

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

    services.AddDbContext<PractiflyContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
}