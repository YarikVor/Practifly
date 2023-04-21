using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PractiFly.DbContextUtility.Context.Users;
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
    public async Task Test1()
    {
        var controller = Mock.Get<CourseController>();

        var result = await controller.UserCourse(1);

        var value = (result as JsonResult).Value;
        
        _output.WriteLine(JsonConvert.SerializeObject(value, Formatting.Indented));
    }
}