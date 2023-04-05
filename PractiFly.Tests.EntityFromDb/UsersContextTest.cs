using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PractiFly.WebApi.Context;

namespace PractiFly.Tests.EntityFromDb;

public class UsersContextTest
{
    static UsersContext _usersContext = Mock.CreateUsersContext();
    private ILogger _logger;
    
    public UsersContextTest(ILogger logger)
    {
        _logger = logger;
    }
    

    
    [Fact]
    public async Task GetUser_NotEmpty()
    {
        var user = await _usersContext.Users.FirstOrDefaultAsync();
        Assert.NotNull(user);
        
        LogJson(user);
        
        NotDefault(user.Id);
        NotDefault(user.FirstName);
        NotDefault(user.LastName);
        NotDefault(user.Email);
    }

    [Fact]
    public async Task GetUserGroup_NotEmpty()
    {
        var userGroup = await _usersContext.UserGroups.AsNoTracking().FirstOrDefaultAsync();
        Assert.NotNull(userGroup);
        
        LogJson(userGroup);
        
        NotDefault(userGroup.UserId);
        NotDefault(userGroup.GroupId);
    }
    
    
    
    
    private static void NotDefault<T>(T value){
        Assert.NotEqual(default(T), value);
    }

    private void LogJson<T>(T obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        _logger.LogInformation(json);
    }
}