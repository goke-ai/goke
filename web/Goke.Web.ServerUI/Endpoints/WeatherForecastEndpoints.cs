using Goke.Web.Data.Models;
using Goke.Web.ServerUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Goke.Web.ServerUI.Endpoints
{
    public static class WeatherForecastEndpoints
    {
        public static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("");

            string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm",
                                    "Balmy", "Hot", "Sweltering", "Scorching" ];

            group.MapGet("/api/weatherforecast", () =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        index,
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();
        }
    }
}
