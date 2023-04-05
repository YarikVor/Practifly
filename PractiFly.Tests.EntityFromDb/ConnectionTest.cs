using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PractiFly.WebApi.Context;
using Xunit.Abstractions;

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