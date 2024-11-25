using Goke.Web.BWAApp.Data;
using Goke.Web.BWAApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace Goke.Web.BWAApp.Services
{
    public class ServerWeatherForecastService(ApplicationDbContext db) : IWeatherForecastService
    {
        public async Task<WeatherForecast[]> GetWeatherForecastsAsync() =>
            await db.WeatherForecasts.ToArrayAsync();
    }
}
