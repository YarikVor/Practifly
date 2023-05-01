using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PractiFly.DbContextUtility.Context.Users;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Controllers;
using PractiFly.WebApi.Dto.Registration;
using Xunit.Abstractions;

namespace PractiFly.WebApi.Tests;

public class UserControllerTest
{
    
    private readonly ITestOutputHelper _output;
    
    public UserControllerTest(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public async Task CreateAndDeleteUser_ValidTokenAndValidOperations()
    {
        // Arrange
        const string email = "createanddelete@test.ua";
        var controller = Mock.Get<UserController>();
        var userManager = Mock.Get<UserManager<User>>();
        var registrationDto = new RegistrationDto()
        {
            Birthday = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-20)),
            Email = email,
            Name = "Create",
            Password = "qqQQ11!!",
            Surname = "Delete",
            Username = "createanddelete",
            Phone = "+380000000000"
        };
        
        // Act
        var result = await controller.Create(registrationDto);
        var findResult = await userManager.FindByEmailAsync(email);
        
        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(findResult);
        Assert.Equal("createanddelete", findResult.UserName);
        
        var okResult = (OkObjectResult)result;
        var tokenString = (string)okResult.Value;
        
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Authorization"] = "Bearer " + tokenString;
        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext,
        };

        var deleteResult = await controller.DeleteCurrentUserAsync();

        Assert.IsType<OkObjectResult>(deleteResult);

        var findResultAfterDelete = await userManager.FindByEmailAsync(email);
        
        Assert.Null(findResultAfterDelete);
    }
    


    
    [Fact]
    public async Task Test4()
    {
        var controller = Mock.Get<CourseController>();

        var result = await controller.GetMaterialsInTheme(1);

        var value = (result as JsonResult).Value;
        
        _output.WriteLine(JsonConvert.SerializeObject(value, Formatting.Indented));
    }
}