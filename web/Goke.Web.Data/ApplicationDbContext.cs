using Goke.Web.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Goke.Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Card> Cards { get; set; } = default!;        
    }
}
