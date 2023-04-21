using AutoMapper;
using Newtonsoft.Json;
using PractiFly.DbEntities.Courses;
using PractiFly.WebApi.AutoMappers;
using PractiFly.WebApi.Dto.CourseDependencies;
using Xunit.Abstractions;

namespace PractiFly.Tests.Mappings;

public class UnitTest1
{
    private readonly ITestOutputHelper _logger;
    public UnitTest1(ITestOutputHelper logger)
    {
        _logger = logger;
    }
    

    
    private void WriteAsJson<T>(T obj)
    {
        string json;
        try
        {
            json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        catch (Exception e)
        {
            json = e.Message;
        }

        _logger.WriteLine(json);
    }

    private void WriteLine(string message)
    {
        _logger.WriteLine(message);
    }
}