using Goke.Web.ServerUI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Goke.Web.ServerUI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Goke.Web.ServerUI.Models.Card> Cards { get; set; } = default!;
        
    }
}
