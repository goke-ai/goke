using Microsoft.EntityFrameworkCore;
using Goke.Web.Data;
using Goke.Web.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace Goke.Web.ServerUI.Endpoints;

public static class CardEndpoints
{
    public static void MapCardEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Card").WithTags(nameof(Card));

        group.MapGet("/", async (ApplicationDbContext db) =>
        {
            return await db.Cards.ToListAsync();
        })
        .WithName("GetAllCards")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Card>, NotFound>> (string id, ApplicationDbContext db) =>
        {
            return await db.Cards.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Card model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCardById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (string id, Card card, ApplicationDbContext db) =>
        {
            var affected = await db.Cards
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, card.Id)
                    .SetProperty(m => m.Pin, card.Pin)
                    .SetProperty(m => m.From, card.From)
                    .SetProperty(m => m.To, card.To)
                    .SetProperty(m => m.Permission, card.Permission)
                    .SetProperty(m => m.OwnerId, card.OwnerId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCard")
        .WithOpenApi();

        group.MapPost("/", async (Card card, ApplicationDbContext db) =>
        {
            db.Cards.Add(card);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Card/{card.Id}", card);
        })
        .WithName("CreateCard")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (string id, ApplicationDbContext db) =>
        {
            var affected = await db.Cards
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCard")
        .WithOpenApi();
    }
}
