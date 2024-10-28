using Microsoft.AspNetCore.Identity;

namespace Goke.AspNetCore.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IList<Card>? Cards { get; set; }

    }
}