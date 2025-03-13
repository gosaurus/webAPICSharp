using Microsoft.AspNetCore.Mvc;
using webAPICSharp.Models;

namespace webAPICSharp.Controllers;

[ApiController]
[Route("[controller]")]

public class DailyForecastController : ControllerBase
{
    private readonly ILogger<DailyForecastController> _logger;
    private readonly WeatherDbContext _context;
    public DailyForecastController
    (
        ILogger<DailyForecastController> logger, 
        WeatherDbContext context
    )
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetDailyForecast")]
    public IEnumerable<DailyForecast> Get()
    {
        return _context.DailyForecasts;
    }
}