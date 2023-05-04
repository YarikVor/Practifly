using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PractiFly.WebApi.Tests;

public class Mock
{
    private static readonly IServiceProvider ServiceProvider = MakeServiceProvider();
    private static IHost host;

    private static IServiceProvider MakeServiceProvider()
    {
        host = Host
                .CreateDefaultBuilder()
                .Use
                .ConfigureWebHostDefaults(webBuilder => webBuilder. < Startup > ())
            .Build();

        return host.Services;
    }

    public static TEntity Get<TEntity>() where TEntity : class
    {
        return ServiceProvider.GetRequiredService<TEntity>();
    }
}