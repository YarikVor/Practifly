using PractiFly.Api.Api.Login;
using PractiFly.Api.Client;
using Xunit.Abstractions;
using System.Text.Json;
namespace TestProject;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;
    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }
    [Fact]
    public async Task Test1()
    {
        var client = new PractiFlyClient("");
        var logindto = new LoginRequestDto() { Email = "svvaleron@gmail.com", Password = "Qwerty@2003" };
        var token = (await client.GetLoginResponseAsync(logindto)).Token;
        client.UpdateToken(token);
        var user = await client.GetUserByIdAsAdminAsync(17);
        WriteAsJson(user);
    }

    public void WriteAsJson(object obj)
    {
        _output.WriteLine(JsonSerializer.Serialize(obj, new JsonSerializerOptions() { WriteIndented = true }));
    }
}