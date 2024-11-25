using Goke.Web.BWAApp.Models;
using System.Net.Http.Json;

namespace Goke.Web.BWAApp.Services
{
    public class ClientWeatherForecastService(HttpClient http) : IWeatherForecastService
    {
        public async Task<WeatherForecast[]> GetWeatherForecastsAsync() =>
            await http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json") ?? [];
    }
}
