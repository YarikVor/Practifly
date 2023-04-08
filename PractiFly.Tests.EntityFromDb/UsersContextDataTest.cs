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
using PractiFly.WebApi.Db.Context;
using PractiFly.WebApi.EntityDb.Materials;
using PractiFly.WebApi.EntityDb.Users;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace PractiFly.Tests.EntityFromDb;

public class UsersContextDataTest
{
    static PractiflyContext _practiflyContext = Mock.CreatePractiflyContext() 
                                                ?? throw new NullReferenceException();
    static FakerManager _fakerManager = new PractiFlyFakerManager();
    private ITestOutputHelper _logger;
    private Checker _checker;
    private const int _countEntity = 5;

    public UsersContextDataTest(ITestOutputHelper logger)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        _logger = logger;
        var option = new CheckerOptionBuilder()
            .Init()
            .SkipSubstring("Note")
            .SkipSubstring("Description")
            .SkipType<bool>()
            .Build();

        _checker = new Checker(option);

        //ClearDb();
    }

    private void ClearDb()
    {
        const string sql =
            @"DO $$ DECLARE tables CURSOR FOR SELECT pg_tables.tablename FROM pg_tables WHERE schemaname = 'public'; BEGIN FOR table_record IN tables LOOP EXECUTE 'TRUNCATE TABLE `' || table_record.tablename || '` RESTART IDENTITY CASCADE;'; END LOOP; FOR table_record IN tables LOOP EXECUTE 'DROP TABLE IF EXISTS `' || table_record.tablename || '` CASCADE;'; END LOOP; END $$;";
        _practiflyContext.Database.ExecuteSqlRaw(sql, ArraySegment<object>.Empty);
    }

    public static object[] MakeTest<T>(
        DbSet<T> dbSet, 
        params Expression<Func<T, object>>[] ignoreProperty
    )
    where T : class
    {
        return new object[] { dbSet, ignoreProperty };
    }
    
    public static IEnumerable<object[]> GetTestData()
    {
        yield return MakeTest(_practiflyContext.Users );
        yield return MakeTest(_practiflyContext.Groups );
        yield return MakeTest(_practiflyContext.Courses);
        yield return MakeTest(_practiflyContext.Levels);
        yield return MakeTest(_practiflyContext.Headings);
        yield return MakeTest(_practiflyContext.Languages);
        yield return MakeTest(_practiflyContext.Materials);
        yield return MakeTest(_practiflyContext.Competencies, c => c.Parent, c => c.ParentId);
        yield return MakeTest(_practiflyContext.Themes);
        yield return MakeTest(_practiflyContext.ThemeMaterials);
        yield return MakeTest(_practiflyContext.CourseCompetencies);
        yield return MakeTest(_practiflyContext.CourseDependencyTypes);
        yield return MakeTest(_practiflyContext.CourseDependencies);
        yield return MakeTest(_practiflyContext.CourseHeadings);
        yield return MakeTest(_practiflyContext.CourseMaterials);
        yield return MakeTest(_practiflyContext.HeadingCompetencies);
        yield return MakeTest(_practiflyContext.HeadingMaterials);
        yield return MakeTest(_practiflyContext.MaterialBlocks);
        yield return MakeTest(_practiflyContext.MaterialCompetencies);
        yield return MakeTest(_practiflyContext.Units);
        yield return MakeTest(_practiflyContext.GroupCourses);
        yield return MakeTest(_practiflyContext.UserCourses, uc => uc.Grade);
        yield return MakeTest(_practiflyContext.UserGroups);
        yield return MakeTest(_practiflyContext.UserHeadings);
        yield return MakeTest(_practiflyContext.UserThemes);
        yield return MakeTest(_practiflyContext.UserMaterials);
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public async Task GetEntity_NotEmpty<TEntity>(DbSet<TEntity> dbSet, Expression<Func<TEntity, object>>[] ignoreProperty)
        where TEntity : class
    {
        AddEntitiesIfEmpty<TEntity>(dbSet);
        
        var entity = await dbSet.FirstAsync();

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

            WriteLine("Fake entities:");
            WriteAsJson(entities);
            
            dbSet.AddRange(entities);

            _practiflyContext.SaveChanges();
        }
    }

    private void WriteAsJson<T>(T obj)
    {
        string json;
        try
        {
            json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        catch (Exception e)
        {
            json = e.Message;
        }

        _logger.WriteLine(json);
    }

    private void WriteLine(string message)
        => _logger.WriteLine(message);
}
