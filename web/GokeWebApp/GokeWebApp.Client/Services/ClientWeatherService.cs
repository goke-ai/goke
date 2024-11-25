using GokeWebApp.Client.Models;
using System.Net.Http.Json;

namespace GokeWebApp.Client.Services
{
    public partial class ClientWeatherService(HttpClient http) : IWeatherService
    {
        public Task<WeatherForecast[]> GetWeatherForecasts() =>
            http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json")!;
    }
}
