using Goke.Web.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Goke.Web.ServerUI.Identity
{
    public class CustomUserValidator : UserValidator<ApplicationUser>
    {
        public override Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            var result = base.ValidateAsync(manager, user);



            return result;
        }
    }
}
