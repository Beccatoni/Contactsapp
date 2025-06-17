
using ContactsApp.Api.Data;

public interface IWeatherForecastRepository
{
    IEnumerable<WeatherForecast> GetForecasts();
}

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly ContactsDbContext _context;

    public WeatherForecastRepository(ContactsDbContext context)
    {
        _context = context;
    }
    
    // private static readonly string[] Summaries = new[]
    // {
    //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    // };

    public IEnumerable<WeatherForecast> GetForecasts()
    {
        return _context.WeatherForecasts.ToList();

    }
}