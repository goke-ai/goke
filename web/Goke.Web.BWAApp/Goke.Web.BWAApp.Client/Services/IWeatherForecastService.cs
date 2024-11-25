using Goke.Web.BWAApp.Models;

namespace Goke.Web.BWAApp.Services
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetWeatherForecastsAsync();
    }
}