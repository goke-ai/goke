using Goke.Web.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Goke.Web.ServerUI.Endpoints
{
    public static class IdentityEndpoints
    {
        public static void MapIdentityEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("");

            // provide an endpoint to clear the cookie for logout
            //
            // For more information on the logout endpoint and antiforgery, see:
            // https://learn.microsoft.com/aspnet/core/blazor/security/webassembly/standalone-with-identity#antiforgery-support
            group.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager, [FromBody] object empty) =>
            {
                if (empty is not null)
                {
                    await signInManager.SignOutAsync();

                    return Results.Ok();
                }

                return Results.Unauthorized();
            }).RequireAuthorization();

            // provide an endpoint for user roles
            group.MapGet("/roles", (ClaimsPrincipal user) =>
            {
                if (user.Identity is not null && user.Identity.IsAuthenticated)
                {
                    var identity = (ClaimsIdentity)user.Identity;
                    var roles = identity.FindAll(identity.RoleClaimType)
                        .Select(c =>
                            new
                            {
                                c.Issuer,
                                c.OriginalIssuer,
                                c.Type,
                                c.Value,
                                c.ValueType
                            });

                    return TypedResults.Json(roles);
                }

                return Results.Unauthorized();
            }).RequireAuthorization();


        }
    }
}
