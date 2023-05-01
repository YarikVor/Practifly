using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PractiFly.DbContextUtility.Context.Courses;
using PractiFly.DbContextUtility.Context.Materials;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbContextUtility.Context.Users;
using PractiFly.WebApi.AutoMappers;
using PractiFly.WebApi.Controllers;

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
            .ConfigureWebHostDefaults(webBuilder => webBuilder.<Startup>())
            .Build();

        return host.Services;
    }

    public static TEntity Get<TEntity>() where TEntity : class
    {
        return ServiceProvider.GetRequiredService<TEntity>();
    }
}