using Microsoft.AspNetCore.Identity;

namespace Goke.Web.ServerUI.Models
{
    public class ApplicationUser : IdentityUser
    {
        IList<Card>? Cards { get; set; }
    }
}