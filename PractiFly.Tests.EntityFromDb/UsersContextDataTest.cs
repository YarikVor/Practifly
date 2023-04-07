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
    static MaterialsContext _materialsContext = Mock.CreateMaterialsContext();
    static CoursesContext _coursesContext = Mock.CreateCoursesContext();


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
    /*
     ÏÎÐßÄÎÊ:
        1.User
        2.Group
        3.Course (Owner)
        4.Level
        5.Heading
        6.Language
        7.Material
        8.Competency (Parent)
        9.Theme (Parent)
        10. ThemeMaterial
        11. CourseCompetency
        12. CourseDependencyType
        13. CourseDependency
        14. CourseHeading
        15. CourseMaterial
        16. HeadingCompetency
        17. HeadingMaterial
        18.  MaterialBlock (Parent, Child)
        19. MaterialCompetency
        20. Unit (?)
        21. GroupCourse
        22. UserCourse
        23. UserGroup
        24. UserHeading
        25. UserTheme
        26. UserMaterial
        27. Role (?)
     */
    // ToDo:
    public static IEnumerable<object[]> GetTestData()
    {
        yield return MakeTest(_usersContext.Users );
        yield return MakeTest(_usersContext.Groups );
        yield return MakeTest(_coursesContext.Courses);
        yield return MakeTest(_materialsContext.Levels);
        yield return MakeTest(_materialsContext.Headings);
        yield return MakeTest(_materialsContext.Languages);
        yield return MakeTest(_materialsContext.Materials);
        yield return MakeTest(_materialsContext.Competencies);
        yield return MakeTest(_materialsContext.Themes);
        yield return MakeTest(_coursesContext.ThemeMaterials);
        yield return MakeTest(_coursesContext.CourseCompetencies);
        yield return MakeTest(_coursesContext.CourseDependencyTypes);
        yield return MakeTest(_coursesContext.CourseDependencies);
        yield return MakeTest(_coursesContext.CourseHeadings);
        yield return MakeTest(_coursesContext.CourseMaterials);
        yield return MakeTest(_materialsContext.HeadingCompetencies);
        yield return MakeTest(_materialsContext.HeadingMaterials);
        yield return MakeTest(_materialsContext.MaterialBlocks);
        yield return MakeTest(_materialsContext.MaterialCompetencies);
        yield return MakeTest(_materialsContext.Units);
        yield return MakeTest(_usersContext.GroupCourses);
        yield return MakeTest(_usersContext.UserCourses, uc => uc.Grade);
        yield return MakeTest(_usersContext.UserGroups);
        yield return MakeTest(_usersContext.UserHeadings);
        yield return MakeTest(_usersContext.UserThemes);
        yield return MakeTest(_usersContext.UserMaterials);
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
