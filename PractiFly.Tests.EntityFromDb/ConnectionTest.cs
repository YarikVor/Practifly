using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PractiFly.WebApi.Context;
using Xunit.Abstractions;
using PractiFly.WebApi.EntityDb.Users;
using Xunit.Abstractions;

namespace PractiFly.Tests.EntityFromDb;

public class ConnectionTest
{
    static UsersContext _usersContext = Mock.CreateUsersContext();
    
    [Fact]
    public async Task TestConnection_Success()
    {
        _usersContext.Update(new User
        {
            //Id = 1,
            FirstName = "Yarik",
            LastName = "Vorobyov",
            Email = "yarikhelov@gmail.com",
            Phone = "+380501234567",
            FilePhoto = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png",
            RegistrationDate = new(2021, 1, 1),
            Note = null
        });
        _usersContext.SaveChanges();
        
        await _usersContext.Database.OpenConnectionAsync();
        await _usersContext.Database.CloseConnectionAsync();
    }
}