using Goke.Web.BWAApp.Client;
using Goke.Web.BWAApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

// set base address for default host
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:5002") });

//// configure client for auth interactions
//builder.Services.AddHttpClient(
//    "Auth",
//    opt => opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:5001"))
//    .AddHttpMessageHandler<CookieHandler>();


builder.Services.AddScoped<IWeatherForecastService, ClientWeatherForecastService>();

await builder.Build().RunAsync();
