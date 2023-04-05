using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.Context;

namespace PractiFly.Tests.EntityFromDb;

public class UsersContextTest
{
    static UsersContext _usersContext = Mock.CreateUsersContext();
    
    [Fact]
    public async Task TestConnection_Success()
    {
        await _usersContext.Database.OpenConnectionAsync();
        await _usersContext.Database.CloseConnectionAsync();
    }
    
    [Fact]
    public async Task GetUser_NotEmpty()
    {
        var user = await _usersContext.Users.FirstOrDefaultAsync();
        Assert.NotNull(user);
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
        NotDefault(userGroup.UserId);
        userGroup.GroupId = 0;
        NotDefault(userGroup.GroupId);
        
        
    }
    
    
    
    
    private static void NotDefault<T>(T value){
        Assert.NotEqual(default(T), value);
    }
}