using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

// Anything in square brackets is an attribute.
[ApiController]
[Route("[controller]")] // "Controller" is a placeholder here - it will route to "WeatherForecast" in the class below, ignoring the "Controller" part.
public class WeatherForecastController : ControllerBase    // The ": ControllerBase" shows inheritance - WeatherForecastController inherits from ControllerBase.
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")] // This is an endpoint, HTTPGET. This is what we will get when we send a GET request to the WeatherForecastController."
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),   // These are properties
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();  // this array is what we saw in swagger.
    }
}
