using Goke.Web.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Goke.Web.Server.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : IdentityDbContext<ApplicationUser>(options)
{

public DbSet<Goke.Web.Server.Models.WeatherForecast> WeatherForecasts { get; set; } = default!;
}