using GokeWebApp.Client.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GokeWebApp.Endpoints
{
    public static class DataProcessingEndpoints
    {
        public static void MapDataProcessingEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api");

            // provide an endpoint example that requires authorization
            group.MapPost("/data-processing-0", ([FromBody] FormModel model) =>
                Results.Text($"{model.Message.Length} characters"))
                    .RequireAuthorization()
                    .AllowAnonymous();

            // provide an endpoint example that requires authorization
            group.MapPost("/data-processing-1", ([FromBody] FormModel model) =>
                Results.Text($"{model.Message.Length} characters"))
                    .RequireAuthorization();

            // provide an endpoint example that requires authorization with a policy
            group.MapPost("/data-processing-2", ([FromBody] FormModel model) =>
                Results.Text($"{model.Message.Length} characters"))
                    .RequireAuthorization(policy => policy.RequireRole("Managers"));

            // provide an endpoint example that requires authorization with a policy
            group.MapPost("/data-processing-3", ([FromBody] FormModel model) =>
                Results.Text($"{model.Message.Length} characters"))
                    .RequireAuthorization(policy => policy.RequireRole("Administrators"));

        }
    }
}
