using Goke.AspNetCore.Identity.Models;
using Goke.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Goke.AspNetCore.Identity
{
    public class UserManager : UserManager<ApplicationUser>
    {
        private const string SEPARATOR = "|||";
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;

        public UserManager(IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApplicationUser>> logger,
            IConfiguration configuration,
            ApplicationDbContext context
            )
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.configuration = configuration;
            this.context = context;
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            // Custom logic before registration
            if (!IdentityManager.IsInRegisteredUsers(configuration, user.UserName))
            {
                // check pin
                if (!IdentityManager.IsNewAndValidPin(context, pin))
                {
                    return IdentityResult.Failed();
                }
            }

            // remove existing account
            var oldUser = await this.FindByNameAsync(user.UserName!);
            if (oldUser is not null)
            {
                await this.DeleteAsync(oldUser);
            }


            user.EmailConfirmed = true;

            // call base
            var result = await base.CreateAsync(user);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(pin))
                {
                    var card = context.Cards.FirstOrDefault(f => f.Pin == pin);
                    if (card is not null)
                    {
                        card.OwnerId = user.Id;
                    }
                    context.SaveChanges();
                }
            }

            return result;
        }

        string pin = string.Empty;

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            // 
            (string pwd, pin) = SplitPassword(password);

            // call base
            var result = await base.CreateAsync(user, pwd);

            // Custom logic after registration
            if (result.Succeeded)
            {
                if (user.Email is not null)
                {
                    var appUser = await FindByEmailAsync(user.Email);
                    if (appUser is not null)
                    {
                        string[] roles = await IdentityManager.RoleListAsync(context, appUser, pin);

                        if (roles is not null)
                        {
                            await AddToRolesAsync(appUser, roles);
                        }
                    }
                }
            }
            else
            {

            }
            return result;
        }


        public static string CombinePassword(string password, string code, string separator = SEPARATOR)
        {
            return $"{password}{separator}{code}";
        }

        public static (string pwd, string pin) SplitPassword(string combinePwd, string separator = SEPARATOR)
        {
            string password = string.Empty;
            string pin = string.Empty;

            var splitPwd = combinePwd.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (splitPwd.Length == 1)
            {
                password = (splitPwd[0]);
            }
            else if (splitPwd.Length == 2)
            {
                (password, pin) = (splitPwd[0], splitPwd[1]);
            }
            return (password, pin);
        }

    }
}
