using PractiFly.Api.Api.Login;
using PractiFly.Api.Client;
using Xunit.Abstractions;
using System.Text.Json;
using PractiFly.Api.Admin;
using System.Net.Http;
using PractiFly.Api.Api.Admin;

namespace TestProject;

public class UnitTest1
{
    private const string BasicUrl = "https://localhost:5001/api/";
    private readonly HttpClient _httpClient = new ();
    private readonly PractiFlyClient PClient = new("");
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

    [Fact]
    public async Task Test2()
    {
        //var query = string.Format("https://localhost:5001/api/admin/user/filter?FirstName={0}", "v");

        //var result = await _httpClient.GetStringAsync(query)
        //    ?? throw new NullReferenceException("result");

        //_output.WriteLine(result);
        UserFilterInfoDto user = new UserFilterInfoDto()
        {
            FirstName = "v",
        };
        await PClient.GetFilterUserAsync(user);


    }

    public void WriteAsJson(object obj)
    {
        _output.WriteLine(JsonSerializer.Serialize(obj, new JsonSerializerOptions() { WriteIndented = true }));
    }
}