using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace PractiFly.WebApi.IntegrationTests;



public class UnitTest1
{
    private readonly ITestOutputHelper _output;
    
    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }
    
    private void WriteLine(string message)
    {
        _output.WriteLine(message);
    }
    
    [Fact]
    public async Task Test1()
    {
        var webHost = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(_ => { });
        
        var client = webHost.CreateClient();

        var result = await client.GetAsync("api/material?materialId=1");
        
        Assert.True(result.IsSuccessStatusCode);
        
        WriteLine(await result.Content.ReadAsStringAsync());
    }
    
    
}