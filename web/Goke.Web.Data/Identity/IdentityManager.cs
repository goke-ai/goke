using Goke.AspNetCore.Identity.Models;
using Goke.Core;
using Goke.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Encodings.Web;

namespace Goke.AspNetCore.Identity
{
    public class IdentityManager
    {
        enum RoleType { Users = 1, Officers = 2, Managers = 4, Administrators = 8, SystemAdministrators = 16 }

        public static string[] Roles => Enum.GetNames<RoleType>();

        public static async Task EnsureAdmins(IConfiguration configuration, 
            UserManager<ApplicationUser> userManager, 
            IEnumerable<SeedUser> adminUsers, 
            IEmailSender<ApplicationUser>? emailSender = null)
        {
            foreach (var user in adminUsers)
            {
                var password = emailSender is not null ?
                    Text.GeneratePin() :
                    configuration.GetValue<string>($"{user.UserName!}:Secret") ?? user.Password;
                if (password != null)
                {
                    user.EmailConfirmed = true;

                    await userManager.CreateAsync(user, password);

                    if (user.Email is not null)
                    {
                        var appUser = await userManager.FindByEmailAsync(user.Email);

                        if (appUser is not null)
                        {
                            if (user.RoleList is not null)
                            {
                                await userManager.AddToRolesAsync(appUser, user.RoleList);
                            }

                            if (emailSender is not null)
                            {
                                var email = configuration.GetValue<string>($"{user.UserName!}:Email") ?? user.Email;
                                await emailSender.SendPasswordResetCodeAsync(user, email, HtmlEncoder.Default.Encode($"{user.UserName!.ToUpper()[0..3]}|{password}"));
                            }
                        }
                    }
                }
            }
        }

        public static async Task EnsureResetAdmins(IConfiguration configuration, 
            UserManager<ApplicationUser> userManager, 
            IEnumerable<SeedUser> adminUsers, 
            IEmailSender<ApplicationUser>? emailSender = null)
        {
            foreach (var user in adminUsers)
            {
                if (user.Email is not null)
                {
                    var appUser = await userManager.FindByEmailAsync(user.Email);
                    if (appUser != null)
                    {
                        await userManager.DeleteAsync(appUser);
                    }
                }
            }
            await EnsureAdmins(configuration, userManager, adminUsers, emailSender);
        }

        public static async Task EnsureRoles(RoleManager<IdentityRole> roleManager)
        {           
            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static bool IsInRegisteredUsers(IConfiguration configuration, string? username)
        {
            var usernames = configuration["RegisteredEmails"]?.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            return usernames?.Any(x => x == username) == true;
        }

        public static bool IsNewAndValidPin(ApplicationDbContext context, string password)
        {
            var card = context.Cards.FirstOrDefault(f => f.Pin == password);

            if (card == null)
                return false;

            if (card.OwnerId == null && card.To > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }

        public static bool HasValidPin(ApplicationDbContext context, string? userName)
        {
            var card = context.Cards.Include(i => i.Owner)
                        .FirstOrDefault(f => f.Owner.UserName == userName);

            if (card == null) return false;

            if (card.To < DateTime.UtcNow) return false;

            return true;
        }

        public static async Task<string[]> RoleListAsync(ApplicationDbContext context, ApplicationUser user, string password)
        {
            int permission = 0;

            var card = context.Cards.FirstOrDefault(f => f.Pin == password);
            if (card != null)
            {
                card.OwnerId = user.Id;
                permission = card.Permission;

                await context.SaveChangesAsync();
            }

            int i = 0;
            switch ((RoleType)permission)
            {
                case RoleType.Users:
                    //i = (int)Math.Sqrt(permission);
                    if (password.Contains("^0")) i = 0;
                    break;
                case RoleType.Officers:
                    if (password.Contains("^1")) i = 1;
                    break;
                case RoleType.Managers:
                    if (password.Contains("^2")) i = 2;
                    break;
                case RoleType.Administrators:
                    if (password.Contains("^3")) i = 3;
                    break;
                case RoleType.SystemAdministrators:
                    if (password.Contains("^4")) i = 4;
                    break;
                default:
                    break;
            }

            return Roles[..(i + 1)];
        }
    }
}
