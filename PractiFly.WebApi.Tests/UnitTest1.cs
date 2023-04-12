using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.Users;
using PractiFly.WebApi.Controllers;
using PractiFly.WebApi.Dto.Registration;

namespace PractiFly.WebApi.Tests;

public class UserControllerTest
{
    [Fact]
    public async Task Test1()
    {
        var controller = Mock.Get<UserController>();
        var userDb = Mock.Get<IUsersContext>().Users;
        // var controller = new UserController(userDb)

        var user = await userDb.AsNoTracking().OrderBy(e => e.Id).LastAsync();

        user.Should().NotBeNull();

        "TExt".Should().Be("tete");

        LoginDto loginDto = new()
        {
            Email = user.Email,
            Password = user.PasswordHash
        };

        var result = await controller.Login(loginDto);

        result
            .Should()
            .NotBeNull()
            .And
            .BeAssignableTo<OkObjectResult>()
            .Subject
            .Value
            .Should()
            .Be(user.FirstName);


        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.Equal(user.FirstName, okResult.Value);
    }
}