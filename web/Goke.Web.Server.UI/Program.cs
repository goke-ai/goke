using Goke.AspNetCore.Identity.Models;
using Goke.AspNetCore.Identity;
using Goke.Web.Data;
using Goke.Web.Server.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Goke.Web.Server.UI.Endpoints;
using Goke.Web.Server.UI.Models;

var builder = WebApplication.CreateBuilder(args);

//// establish cookie authentication
//builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

//// Configure app cookie
////
//// The default values, which are appropriate for hosting the Backend and
//// BlazorWasmAuth apps on the same domain, are Lax and SameAsRequest. 
//// For more information on these settings, see:
//// https://learn.microsoft.com/aspnet/core/blazor/security/webassembly/standalone-with-identity#cross-domain-hosting-same-site-configuration
///*
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Cookie.SameSite = SameSiteMode.Lax;
//    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
//});
//*/

//// configure authorization
//builder.Services.AddAuthorizationBuilder();

// add the database
//
bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
bool isLinux = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
bool isOSX = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

// Add services to the container.

DatabaseType? databaseType = DatabaseType.MSSQL;
if (isWindows)
{
    databaseType = DatabaseType.MSSQL;
}
if (isLinux)
{
    databaseType = DatabaseType.MySQL;
}

// uncomment below line to generate MySQL migration
// databaseType = DatabaseType.MySQL;

var connectionString = string.Empty;
switch (databaseType)
{
    case DatabaseType.MSSQL:
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Goke.Web.Data.MSSQLMigrations")));
        break;

    case DatabaseType.MySQL:
        connectionString = builder.Configuration.GetConnectionString("MariaDBDefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        var serverVersion = new MariaDbServerVersion(new Version(8, 0, 2));

        //builder.Services.AddDbContext<ApplicationDbContext>(options =>
        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, serverVersion, b => b.MigrationsAssembly("Goke.Web.Data.MySQLMigrations")));

        break;
    case DatabaseType.Sqlite:
        //builder.Services.AddScoped<AppIdentityDbContext>();
        //builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
        //    options.UseSqlite($"Data Source={nameof(Goke.Web)}.db"));
        break;
    default:
        break;
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole("SystemAdministrators", "Administrators"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Admin", "/", "RequireAdministratorRole");
});

//+--------
// add a CORS policy for the client
builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins([
            builder.Configuration["BackendUrl"] ?? "https://localhost:7202",
            builder.Configuration["FrontendUrl"] ?? "https://localhost:7056"
            ])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddTransient<IEmailSender<ApplicationUser>, EmailSender>();

//---------


// builder.Services.AddTransient<IUserValidator<ApplicationUser>, Goke.AspNetCore.Identity.CustomUserValidator>();
builder.Services.AddScoped<SignInManager<ApplicationUser>, SignInManager>();
builder.Services.AddScoped<UserManager<ApplicationUser>, UserManager>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    //+--------

    app.UseSwagger();
    app.UseSwaggerUI();
    //--------
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

//+-------------
SeedData.InitializeAsync(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};
//--------------


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

//+-----------
// create routes for the identity endpoints
app.MapIdentityApi<ApplicationUser>();

// activate the CORS policy
app.UseCors("wasm");

// Enable authentication and authorization after CORS Middleware
// processing (UseCors) in case the Authorization Middleware tries
// to initiate a challenge before the CORS Middleware has a chance
// to set the appropriate headers.
app.UseAuthentication();
app.UseAuthorization();

// Identity Endpoints
app.MapIdentityEndpoints();

// DataProcessing Endpoints
app.MapDataProcessingEndpoints();

// WeatherForecast Endpoints
app.MapWeatherForecastEndpoints();

//--------------

app.MapRazorPages();

app.Run();
