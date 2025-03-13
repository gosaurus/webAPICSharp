using webAPICSharp.Models;

namespace webAPICSharp.data 
{
    public static class SeedData
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", 
                "Hot", "Sweltering", "Scorching"
        };

        private static readonly string[] PrecipitationArray = new[]
        {
            "Snow", "Hail", "Sleet", "Heavy rain", "Rain", "Showers", "Drizzle", "Dry"
        };


        public static void Initialise(WeatherDbContext context) 
        {
            if (context == null)
            {
                throw new ArgumentNullException("Dbcontext is null.");
            }

            if (!context.DailyForecasts.Any())
            {
               //seed random forcasts;
                var dailyForecastsToSeed = Enumerable.Range(1,50)
                .Select(index => new DailyForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Celcius  = Random.Shared.Next(-20,55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    Precipitation = PrecipitationArray[
                        Random.Shared.Next(PrecipitationArray.Length)]
                })
                .ToArray();

                foreach (var forecastToSeed in dailyForecastsToSeed)
                {
                    context.DailyForecasts.Add(forecastToSeed);
                    context.SaveChanges();
                }
            }     
        }
    }
}