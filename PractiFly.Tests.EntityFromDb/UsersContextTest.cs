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


        /*_usersContext.Users.Add(new()
        {
            FirstName = "Test1",
            LastName = "Test1",
            Email = "",
            Phone = "",
            FilePhoto = "",
            RegistrationDate = DateOnly.FromDateTime(DateTime.Now),
        });*/

        //_usersContext.SaveChanges();

    }


    [Fact]
    public void TestFaker()
    {
        FakerManager fakerManager = new PractiFlyFakerManager();
        
        var user = fakerManager.Generate<User>(4);
        
        WriteAsJson(user);
        
        var group = fakerManager.Generate<Group>(1);
        
        WriteAsJson(group);
        
    }
    
    
    [Fact]
    public async Task GetUser_NotEmpty()
    {
        {
            var listUser = new UserFaker().Generate(2);
            
            WriteAsJson(listUser);

            var userNew = listUser[0];

            userNew.Id = 1;

            _usersContext.Users.Add(userNew);
            _usersContext.SaveChanges();            
        }
        
        
        var user = await _usersContext.Users.OrderBy(e => e.Id).LastAsync();
        Assert.NotNull(user);
        
        WriteAsJson(user);
        
        _checker.Check(user);
    }

    [Fact]
    public async Task GetUserGroup_NotEmpty()
    {
        var userGroup = await _usersContext
            .UserGroups
            .AsNoTracking()
            .Include(e => e.User)
            .FirstOrDefaultAsync();
        
        Assert.NotNull(userGroup);
        
        
        WriteAsJson(userGroup);
        
        NotDefault(userGroup.UserId);
        NotDefault(userGroup.GroupId);
    }

    [Fact]
    public async Task GetUserCourse_NotEmpty()
    {
        var userCourse = await _usersContext
            .UserCourses
            .AsNoTracking()
            .FirstOrDefaultAsync();
        Assert.NotNull(userCourse);
        Assert.NotEqual(0, userCourse.CourseId);
       
        WriteLine("UserCourse:");
        WriteAsJson(userCourse);

        NotDefault(userCourse.UserId);
        NotDefault(userCourse.CourseId);
        NotDefault(userCourse.LevelId);
        NotDefault(userCourse.IsCompleted);
        NotDefault(userCourse.LastThemeId);
        NotDefault(userCourse.LastTime);
        NotDefault(userCourse.Grade);
        
        
    }
    [Fact]
    public async Task GetUserHeading_NotEmpty()
    {
        var userHeading = await _usersContext
            .UserHeadings
            .AsNoTracking()
            .FirstOrDefaultAsync();
        Assert.NotNull(userHeading);
        Assert.NotEqual(0, userHeading.UserId);

        WriteLine("UserHeading:");
        WriteAsJson(userHeading);

        NotDefault(userHeading.UserId);
        NotDefault(userHeading.LevelId);
        NotDefault(userHeading.HeadingId);
    }

    [Fact]
    public async Task GetUserMaterial_NotEmpty()
    {
        var userMaterial = await _usersContext
            .UserMaterials
            .AsNoTracking()
            .FirstOrDefaultAsync();
        Assert.NotNull(userMaterial);
        Assert.NotEqual(0, userMaterial.UserId);

        WriteLine("UserMaterial:");
        WriteAsJson(userMaterial);

        NotDefault(userMaterial.UserId);
        NotDefault(userMaterial.MaterialId);
        NotDefault(userMaterial.IsCompleted);
        NotDefault(userMaterial.ResultUrl);
        NotDefault(userMaterial.Grade);
    }
    [Fact]
    public async Task GetUserTheme_NotEmpty()
    {
        var userTheme = await _usersContext
            .UserThemes
            .AsNoTracking()
            .FirstOrDefaultAsync();
        Assert.NotNull(userTheme);
        Assert.NotEqual(0, userTheme.UserId);

        WriteLine("UserTheme:");
        WriteAsJson(userTheme);

        NotDefault(userTheme.UserId);
        NotDefault(userTheme.ThemeId);
        NotDefault(userTheme.IsCompleted);
        NotDefault(userTheme.LevelId);
        NotDefault(userTheme.Grade);
    }
    [Fact]
    public async Task GetGroup_NotEmpty()
    {
        var group = await _usersContext
            .Groups
            .AsNoTracking()
            .FirstOrDefaultAsync();
        Assert.NotNull(group);

        WriteLine("Group:");
        WriteAsJson(group);

        NotDefault(group.Id);
        NotDefault(group.Name);
        NotDefault(group.FoundationDate);
        NotDefault(group.TerminationDate);
    }
    [Fact]
    public async Task GetGroupCourse_NotEmpty()
    {
        var group = await _usersContext
            .GroupCourses
            .AsNoTracking()
            .FirstOrDefaultAsync();
        Assert.NotNull(group);

        WriteLine("GroupCourse:");
        WriteAsJson(group);

        NotDefault(group.GroupId);
        NotDefault(group.CourseId);
        NotDefault(group.LevelId);
        NotDefault(group.IsCompleted);
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