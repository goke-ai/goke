using Goke.Web.Data;
using Goke.Web.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Goke.Web.ServerUI.Identity
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;

        public CustomUserManager(IUserStore<ApplicationUser> store, 
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

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            // Custom logic before registration
            if (!IsInRegisteredUsers(user.UserName))
            {
                // check pin
                if (!IsValidPin(password))
                {
                    return IdentityResult.Failed();
                }
            }

            // call base
            var result = await base.CreateAsync(user, password);

            // Custom logic after registration


            return result;
        }

        private bool IsInRegisteredUsers(string? username)
        {
            var usernames = configuration["RegisteredEmails"]?.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            return usernames?.Any(x => x == username) == true;
        }

        private bool IsValidPin(string password)
        {
            var card = context.Cards.FirstOrDefault(f => f.Pin == password);

            if (card == null)
                return false;

            if (card.OwnerId == null && card.To > DateTime.UtcNow) 
                return true;

            return false;
        }

    }
}
