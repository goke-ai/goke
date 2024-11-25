using Goke.Web.Server.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Goke.Web.Server.UI.Endpoints
{
    public static class BuyPinEndpoints
    {
        public static void MapBuyPinEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("api");

            // provide an endpoint example that requires authorization
            group.MapPost("/buy", ([FromBody] FormModel model) =>
                Results.Text($"{model.Message.Length} characters"));


        }
    }
}
