using GokeWebApp.Client;
using GokeWebApp.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

//// Preconfigure an HttpClient for web API calls
//builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register the client-side weather service
builder.Services.AddScoped<IWeatherService, ClientWeatherService>();
builder.Services.AddScoped<IDataProcessingService, ClientDataProcessingService>();

// Create a preconfigured HttpClient with base address for the Web API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:5002") });

//// Create a preconfigured named HttpClient with base address for named client component example
//builder.Services.AddHttpClient("WebAPI", client => client.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:5002"));


await builder.Build().RunAsync();
