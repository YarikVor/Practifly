using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Xml.XPath;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Practifly.Checkers;
using Practifly.Checkers.Builder;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.EntityDb.Users;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace PractiFly.Tests.EntityFromDb;

public class UsersContextDataTest
{
    static UsersContext _usersContext = Mock.CreateUsersContext();

    private ITestOutputHelper _logger;

    private Checker _checker;

    public UsersContextDataTest(ITestOutputHelper logger)
    {
        _logger = logger;
        var option = new CheckerOptionBuilder()
            .Init()
            .SkipSubstring("Note")
            .SkipSubstring("Description")
            .Build();

        _checker = new Checker(option);
    }

    public static object[] MakeTest<T>(DbSet<T> dbSet, params Expression<Func<T, object>>[] ignoreProperty)
        where T : class
    {
        return new object[] { dbSet, ignoreProperty };
    }

    public static IEnumerable<object[]> GetTestData()
    {
        yield return MakeTest(_usersContext.Users);
        yield return MakeTest(_usersContext.Groups);
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public async Task GetEntity_NotEmpty<T>(DbSet<T> dbSet, Expression<Func<T, object>>[] ignoreProperty)
        where T : class
    {
        var entity = await dbSet.FindAsync(1);

        dbSet.ExecuteDelete();
        
        
        
        Assert.NotNull(entity);

        WriteLine(typeof(T).Name);
        WriteAsJson(entity);

        _checker.Check(entity, ignoreProperty);
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
