using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Practifly.Checkers;
using Practifly.Checkers.Builder;
using Practifly.GeneratorTestData;
using PractiFly.WebApi.Db.Context;
using Xunit.Abstractions;

namespace PractiFly.Tests.EntityFromDb;

public class EntitiesTest
{
    static readonly PractiflyContext _practiflyContext 
        = Mock.CreatePractiflyContext()
          ?? throw new NullReferenceException();

    static  FakerManager _fakerManager = new PractiFlyFakerManager();
    private ITestOutputHelper _logger;
    private Checker _checker;
    private const int CountEntitiesForGenerate = 5;

    public EntitiesTest(ITestOutputHelper logger)
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

    private static object[] MakeTest<T>(DbSet<T> dbSet, params Expression<Func<T, object>>[] ignoreProperty)
        where T : class
    {
        return new object[] { dbSet, ignoreProperty };
    }

    public static IEnumerable<object[]> DbContextData => new[]
    {
        MakeTest(_practiflyContext.Users),
        MakeTest(_practiflyContext.Groups),
        MakeTest(_practiflyContext.Courses),
        MakeTest(_practiflyContext.Levels),
        MakeTest(_practiflyContext.Headings),
        MakeTest(_practiflyContext.Languages),
        MakeTest(_practiflyContext.Materials),
        MakeTest(_practiflyContext.Competencies, c => c.Parent, c => c.ParentId),
        MakeTest(_practiflyContext.Themes),
        MakeTest(_practiflyContext.ThemeMaterials),
        MakeTest(_practiflyContext.CourseCompetencies),
        MakeTest(_practiflyContext.CourseDependencyTypes),
        MakeTest(_practiflyContext.CourseDependencies),
        MakeTest(_practiflyContext.CourseHeadings),
        MakeTest(_practiflyContext.CourseMaterials),
        MakeTest(_practiflyContext.HeadingCompetencies),
        MakeTest(_practiflyContext.HeadingMaterials),
        MakeTest(_practiflyContext.MaterialBlocks),
        MakeTest(_practiflyContext.MaterialCompetencies),
        MakeTest(_practiflyContext.Units),
        MakeTest(_practiflyContext.GroupCourses),
        MakeTest(_practiflyContext.UserCourses, uc => uc.Grade),
        MakeTest(_practiflyContext.UserGroups),
        MakeTest(_practiflyContext.UserHeadings),
        MakeTest(_practiflyContext.UserThemes),
        MakeTest(_practiflyContext.UserMaterials)
    };
    

    [Theory]
    [MemberData(nameof(DbContextData))]
    public async Task DbContext_GetEntity_NotEmpty<TEntity>(DbSet<TEntity> dbSet,
        Expression<Func<TEntity, object>>[] ignoreProperty)
        where TEntity : class
    {
        await AddEntitiesIfEmpty<TEntity>(dbSet);

        var entity = await dbSet.FirstAsync();

        Assert.NotNull(entity);

        WriteLine(typeof(TEntity).Name);
        WriteAsJson(entity);

        _checker.Check(entity, ignoreProperty);
    }

    private async Task AddEntitiesIfEmpty<TEntity>(DbSet<TEntity> dbSet) where TEntity : class
    {
        if (dbSet.Count() < CountEntitiesForGenerate)
        {
            await dbSet.ExecuteDeleteAsync();

            var entities = _fakerManager.Generate<TEntity>(CountEntitiesForGenerate);

            WriteLine("Fake entities:");
            WriteAsJson(entities);

            await dbSet.AddRangeAsync(entities);

            await _practiflyContext.SaveChangesAsync();
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