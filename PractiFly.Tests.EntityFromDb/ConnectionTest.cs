using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.Context;

namespace PractiFly.Tests.EntityFromDb;

public class ConnectionTest
{
    static UsersContext _usersContext = Mock.CreateUsersContext();
    
    [Fact]
    public async Task TestConnection_Success()
    {
        await _usersContext.Database.OpenConnectionAsync();
        await _usersContext.Database.CloseConnectionAsync();
    }
}