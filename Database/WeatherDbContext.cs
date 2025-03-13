using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace webAPICSharp.Models
{
    //This fancy object controls access to the DB 
    public class WeatherDbContext : DbContext
    {
        //App must expose a public constructor
        public WeatherDbContext() : base() {
            Console.WriteLine("empty constructor ran instead");
        }
        public DbSet<DailyForecast> DailyForecasts { get; set; }

        //Configure to default connnection
        protected readonly IConfiguration Configuration;
        public WeatherDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
                Console.WriteLine("override config db with string");
            }
        }

        
    }

}