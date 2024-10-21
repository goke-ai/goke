using Microsoft.AspNetCore.Identity;

namespace Goke.Web.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IList<Card>? Cards { get; set; }
    }
}