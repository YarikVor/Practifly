using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Xml.XPath;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Practifly.Checkers;
using Practifly.Checkers.Builder;
using Practifly.GeneratorTestData;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.EntityDb.Materials;
using PractiFly.WebApi.EntityDb.Users;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace PractiFly.Tests.EntityFromDb;

public class UsersContextDataTest
{
    static UsersContext _usersContext = Mock.CreateUsersContext();
    
    static FakerManager _fakerManager = new PractiFlyFakerManager();

    private ITestOutputHelper _logger;

    private Checker _checker;
    
    private const int _countEntity = 5;

    public UsersContextDataTest(ITestOutputHelper logger)
    {
        _logger = logger;
        var option = new CheckerOptionBuilder()
            .Init()
            .SkipSubstring("Note")
            .SkipSubstring("Description")
            .SkipType<bool>()
            .Build();

        _checker = new Checker(option);
    }

    public static object[] MakeTest<T>(DbSet<T> dbSet, params Expression<Func<T, object>>[] ignoreProperty)
        where T : class
    {
        return new object[] { dbSet, ignoreProperty };
    }

    // ToDo:
    public static IEnumerable<object[]> GetTestData()
    {
        yield return MakeTest(_usersContext.Users );
        yield return MakeTest(_usersContext.Groups );

        yield return MakeTest(_usersContext.UserCourses, uc => uc.Grade);
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public async Task GetEntity_NotEmpty<TEntity>(DbSet<TEntity> dbSet, Expression<Func<TEntity, object>>[] ignoreProperty)
        where TEntity : class
    {
        AddEntitiesIfEmpty<TEntity>(dbSet);
        
        var entity = await dbSet.FindAsync(1);

        Assert.NotNull(entity);

        WriteLine(typeof(TEntity).Name);
        WriteAsJson(entity);

        _checker.Check(entity, ignoreProperty);
    }

    private void AddEntitiesIfEmpty<TEntity>(DbSet<TEntity> dbSet) where TEntity : class
    {
        
        if (dbSet.Count() < _countEntity)
        {
            dbSet.ExecuteDelete();
            
            var entities = _fakerManager.Generate<TEntity>(_countEntity);
            
            dbSet.AddRange(entities);
            
            _usersContext.SaveChanges();
        }
    }


    private void WriteAsJson<T>(T obj)
    {
        var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        _logger.WriteLine(json);
    }

    private void WriteLine(string message)
        => _logger.WriteLine(message);
}
