using GokeWebApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Goke.AspNetCore.Identity
{
    public class SignInManager : SignInManager<ApplicationUser>
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;

        public SignInManager(UserManager<ApplicationUser> userManager, 
            IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, 
            IOptions<IdentityOptions> optionsAccessor, 
            ILogger<SignInManager<ApplicationUser>> logger, 
            IAuthenticationSchemeProvider schemes, 
            IUserConfirmation<ApplicationUser> confirmation,
            IConfiguration configuration,
            ApplicationDbContext context
            ) 
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            this.configuration = configuration;
            this.context = context;
        } 

        public override async Task<bool> CanSignInAsync(ApplicationUser user)
        {
            //
            if (!IdentityManager.IsInRegisteredUsers(configuration, user.UserName))
            {
                if (!IdentityManager.HasValidPin(context, user.UserName))
                {
                    return false;
                }
            }

            var result = await base.CanSignInAsync(user);

            return result;
        }

    }
}
