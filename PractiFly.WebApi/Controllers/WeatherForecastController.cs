using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.Context;

namespace PractiFly.WebApi.Controllers
{

  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : Controller
  {
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly PractiflyContext _context;
    
    public WeatherForecastController(
      ILogger<WeatherForecastController> logger,
      PractiflyContext context
      )
    {
      _logger = logger;
      _context = context;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      })
      .ToArray();
    }
    
    [HttpGet("Test")]
    public async Task<IActionResult> Test()
    {
      var headings = await _context.Headings.FirstAsync();
      
      return Json(headings);
    }
  }
}