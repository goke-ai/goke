using GokeWebApp.Client.Models;

namespace GokeWebApp.Client.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecast[]> GetWeatherForecasts();
    }
}