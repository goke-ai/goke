using Microsoft.EntityFrameworkCore;
using Goke.Web.Server.Data;
using Goke.Web.Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace Goke.Web.Server.Endpoints;

public static class WeatherForecastEndpoints
{
    public static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/WeatherForecast").WithTags(nameof(WeatherForecast));

        group.MapGet("/", async (ApplicationDbContext db) =>
        {
            return await db.WeatherForecasts.ToListAsync();
        })
        .WithName("GetAllWeatherForecasts")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<WeatherForecast>, NotFound>> (int id, ApplicationDbContext db) =>
        {
            return await db.WeatherForecasts.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is WeatherForecast model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetWeatherForecastById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, WeatherForecast weatherForecast, ApplicationDbContext db) =>
        {
            var affected = await db.WeatherForecasts
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, weatherForecast.Id)
                    .SetProperty(m => m.Date, weatherForecast.Date)
                    .SetProperty(m => m.TemperatureC, weatherForecast.TemperatureC)
                    .SetProperty(m => m.Summary, weatherForecast.Summary)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateWeatherForecast")
        .WithOpenApi();

        group.MapPost("/", async (WeatherForecast weatherForecast, ApplicationDbContext db) =>
        {
            db.WeatherForecasts.Add(weatherForecast);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/WeatherForecast/{weatherForecast.Id}", weatherForecast);
        })
        .WithName("CreateWeatherForecast")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ApplicationDbContext db) =>
        {
            var affected = await db.WeatherForecasts
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteWeatherForecast")
        .WithOpenApi();
    }
}
