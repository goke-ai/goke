using GokeWebApp.Client.Models;
using GokeWebApp.Client.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GokeWebApp.Services
{
    public partial class ServerWeatherService(HttpClient http, ILogger<ServerWeatherService> logger, IWebHostEnvironment env) : IWeatherService
    {
        private readonly HttpClient http = http;
        private readonly ILogger<ServerWeatherService> logger = logger;
        private readonly IWebHostEnvironment env = env;

        public async Task<WeatherForecast[]> GetWeatherForecasts()
        {
            //return await Simulate();
            return await LoadFromApiAsync();
        }

        private static async Task<WeatherForecast[]> Simulate()
        {
            // Simulate asynchronous loading to demonstrate streaming rendering
            await Task.Delay(500);

            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            }).ToArray();

            return forecasts;
        }

        private async Task<WeatherForecast[]> LoadFromFileAsync()
        {
            try
            {
                string jsonFilePath = Path.Combine(env.WebRootPath, $"sample-data/weather.json");

                var jsonText = await  File.ReadAllTextAsync(jsonFilePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(jsonText, options);
                return forecasts ?? [];
            }
            catch (FileNotFoundException ex)
            {
                //jsonText = "Data file not found.";
                logger.LogError(ex, "'wwwroot/sample-data/weather.json' not found.");
            }

            return [];
        }

        private async Task<WeatherForecast[]> LoadFromApiAsync()
        {
            try
            {
                var forecasts = await http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
                return forecasts ?? [];
            }
            catch (Exception ex)
            {
                
            }

            return [];
        }



    }
}
