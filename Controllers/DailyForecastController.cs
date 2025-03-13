using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using webAPICSharp.Models;
using webAPICSharp.Utils;

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

    [HttpGet(Name = "PaginatedForecasts")]
    public async Task<ActionResult<IEnumerable<DailyForecast>>> Get
    (
        [FromQuery] QueryParameters queryParameters
    )
    {
        var allForecasts = 
            await _context.DailyForecasts
            .OrderByDescending(dailyForecast => dailyForecast.Id)
            .Skip((queryParameters.pageNumber - 1) * queryParameters.pageSize)
            .Take(queryParameters.pageSize)
            .ToListAsync();

        var forecastCount = await _context.DailyForecasts.CountAsync();
        var response = new
        {
            TotalDays = forecastCount,
            PageSize = queryParameters.pageSize,
            PageNumber = queryParameters.pageNumber,
            Results = allForecasts

        };

        return Ok(response);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<DailyForecast>> GetForecast(int id)
    {
        var theForecast = await _context.DailyForecasts
            .FirstOrDefaultAsync(forecast => forecast.Id == id);
        if (theForecast == null)
        {
            return NotFound();
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