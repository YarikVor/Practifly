using System.Diagnostics;

namespace PractiFly.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        // TODO: Add support DateTime, DateOnly and TimeOnly where UTC = +00:00
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}

