using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

///+
///// establish cookie authentication
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

// Configure app cookie
//
// The default values, which are appropriate for hosting the Backend and
// BlazorWasmAuth apps on the same domain, are Lax and SameAsRequest. 
// For more information on these settings, see:
// https://learn.microsoft.com/aspnet/core/blazor/security/webassembly/standalone-with-identity#cross-domain-hosting-same-site-configuration

//builder.Services.ConfigureApplicationCookie(options =>
//{
//// default
//    options.Cookie.SameSite = SameSiteMode.Lax;
//    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
//// different domain
    //options.Cookie.SameSite = SameSiteMode.None;
    //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//});


// configure authorization
builder.Services.AddAuthorizationBuilder();

// add the database 
//(in memory for the sample)
builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseInMemoryDatabase("AppDb");
        //For debugging only: options.EnableDetailedErrors(true);
        //For debugging only: options.EnableSensitiveDataLogging(true);
    });
////mssql
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(connectionString));
////builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// add identity and opt-in to endpoints
builder.Services.AddIdentityCore<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

// add a CORS policy for the client
builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:5001",
            builder.Configuration["FrontendUrl"] ?? "https://localhost:5002"])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

// add NSwag services
//builder.Services.AddOpenApiDocument();
///-

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    ///+
    // seed the database
    await using var scope = app.Services.CreateAsyncScope();
    await SeedData.InitializeAsync(scope.ServiceProvider);

    // add OpenAPI/Swagger generator and the Swagger UI
    // app.UseOpenApi();
    ///-

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseRouting();

app.MapFallbackToFile("index.html");


///+
// create routes for the identity endpoints
app.MapIdentityApi<AppUser>();

// activate the CORS policy
app.UseCors("wasm");

// Enable authentication and authorization after CORS Middleware
// processing (UseCors) in case the Authorization Middleware tries
// to initiate a challenge before the CORS Middleware has a chance
// to set the appropriate headers.
app.UseAuthentication();
app.UseAuthorization();

// provide an endpoint to clear the cookie for logout
//
// For more information on the logout endpoint and antiforgery, see:
// https://learn.microsoft.com/aspnet/core/blazor/security/webassembly/standalone-with-identity#antiforgery-support
app.MapPost("/logout", async (SignInManager<AppUser> signInManager, [FromBody] object empty) =>
{
    if (empty is not null)
    {
        await signInManager.SignOutAsync();

        return Results.Ok();
    }

    return Results.Unauthorized();
}).RequireAuthorization();

app.UseHttpsRedirection();

// provide an endpoint for user roles
app.MapGet("/roles", (ClaimsPrincipal user) =>
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

// provide an endpoint example that requires authorization
app.MapPost("/data-processing-1", ([FromBody] FormModel model) =>
    Results.Text($"{model.Message.Length} characters"))
        .RequireAuthorization();

// provide an endpoint example that requires authorization with a policy
app.MapPost("/data-processing-2", ([FromBody] FormModel model) =>
    Results.Text($"{model.Message.Length} characters"))
        .RequireAuthorization(policy => policy.RequireRole("Manager"));
///-


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Identity database
public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{
}

// example form model
public class FormModel
{
    public string Message { get; set; } = string.Empty;
}

public class AppUser : IdentityUser
{
}

public class SeedData
{
    private static readonly IEnumerable<SeedUser> seedUsers =
    [
        new SeedUser()
        {
            Email = "leela@contoso.com",
            NormalizedEmail = "LEELA@CONTOSO.COM",
            NormalizedUserName = "LEELA@CONTOSO.COM", 
            RoleList = [ "Administrator", "Manager" ],
            UserName = "leela@contoso.com"
        },
        new SeedUser()
        {
            Email = "harry@contoso.com",
            NormalizedEmail = "HARRY@CONTOSO.COM",
            NormalizedUserName = "HARRY@CONTOSO.COM",
            RoleList = [ "User" ],
            UserName = "harry@contoso.com"
        },
    ];

    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

        //await context.Database.EnsureDeletedAsync();
        //await context.Database.MigrateAsync();

        if (context.Users.Any())
        {
            return;
        }

        var userStore = new UserStore<AppUser>(context);
        var password = new PasswordHasher<AppUser>();

        using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = ["Administrator", "Manager", "User"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        using var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        foreach (var user in seedUsers)
        {
            var hashed = password.HashPassword(user, "Passw0rd!");
            user.PasswordHash = hashed;
            await userStore.CreateAsync(user);

            if (user.Email is not null)
            {
                var appUser = await userManager.FindByEmailAsync(user.Email);

                if (appUser is not null && user.RoleList is not null)
                {
                    await userManager.AddToRolesAsync(appUser, user.RoleList);
                }
            }
        }

        await context.SaveChangesAsync();
    }

    private class SeedUser : AppUser
    {
        public string[]? RoleList { get; set; }
    }
}