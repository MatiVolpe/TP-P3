using Microsoft.AspNetCore.Mvc;

namespace TP_MatiasVolpe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}


public class Person
{
    public int IdPerson { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}

public class Employee : Person
{
    public string Position { get; set; }
    public string Shift { get; set; }
}

public class Supplier : Person
{
    public string Company { get; set; }
}

public class Product
{
    public int IdProduct { get; set; }
    public string ProductName { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
    public int IdPerson { get; set; } 
}

public class Delivery
{
    public int IdDelivery { get; set; }
    public DateTime Date { get; set; }
    public int IdPerson { get; set; }
    public int IdProduct { get; set; }
}

