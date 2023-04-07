using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Practifly.Checkers;
using Practifly.Checkers.Builder;
using Practifly.GeneratorTestData;
using Practifly.GeneratorTestData.Faker.Users;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.EntityDb.Users;
using Xunit;
using Xunit.Abstractions;

namespace PractiFly.Tests.EntityFromDb;

public class UsersContextTest
{
    static UsersContext _usersContext = Mock.CreateUsersContext();
    private ITestOutputHelper _logger;
    
    private Checker _checker;

    public UsersContextTest(ITestOutputHelper logger)
    {
        _logger = logger;
        var option = new CheckerOptionBuilder()
        .Init()
        .SkipSubstring("Note")
        .SkipSubstring("Description")
        .Build();
        
        _checker = new Checker(option);

    }



    private static void NotDefault<T>(T value){
        Assert.NotEqual(default(T), value);
    }

    private void WriteAsJson<T>(T obj)
    {
        var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        _logger.WriteLine(json);
    }

    private void WriteLine(string message) 
        => _logger.WriteLine(message);
    private void WriteLine(string format, params object[] args) 
        => _logger.WriteLine(format, args);
}