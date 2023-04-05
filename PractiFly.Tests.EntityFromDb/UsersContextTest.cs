using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PractiFly.WebApi.Context;
using Xunit.Abstractions;

namespace PractiFly.Tests.EntityFromDb;

public class UsersContextTest
{
    static UsersContext _usersContext = Mock.CreateUsersContext();
    private ITestOutputHelper _logger;
    
    public UsersContextTest(ITestOutputHelper logger)
    {
        _logger = logger;
    }
    
    [Fact]
    public async Task GetUser_NotEmpty()
    {
        var user = await _usersContext.Users.FirstAsync(e => e.Id == 2);
        Assert.NotNull(user);
        
        WriteAsJson(user);
        
        NotDefault(user.Id);
        NotDefault(user.FirstName);
        NotDefault(user.LastName);
        NotDefault(user.Email);
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
        var userCourse = (dynamic)(new object()); //await _usersContext.UserCourses.AsNoTracking().FirstOrDefaultAsync();
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