using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [HttpGet(Name = "GetAllForecasts")]
    public IEnumerable<DailyForecast> Get()
    {

        return _context.DailyForecasts;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<DailyForecast>> GetForecast(int id)
    {
        var theForecast = await _context.DailyForecasts
            .FirstOrDefaultAsync(forecast => forecast.Id == id);
        if (theForecast == null)
        {
            return NotFound(theForecast);
        }
        return Ok(theForecast);
    }

    [HttpPost(Name = "CreateForecast")]
    public async Task<ActionResult<DailyForecast>> CreateForecast(DailyForecast dailyForecast) 
    {
        if (dailyForecast == null) 
        {
            return BadRequest();
        }
        
        // does not check for duplicate because it's a weather forecast
        _context.DailyForecasts.Add(dailyForecast);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetForecast), new { id = dailyForecast.Id }, dailyForecast);
    }
}