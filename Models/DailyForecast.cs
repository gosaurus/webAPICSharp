namespace webAPICSharp;

public class DailyForecast
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public required int Celcius { get; set; }
    public int Fahrenheit => 32 + (int)(Celcius/0.5556);
    public required string Summary { get; set; }
    public required string Precipitation { get; set; }
}