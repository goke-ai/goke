
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Goke.Web.ServerUI.Models;
using Goke.Web.ServerUI.Data;
using Microsoft.Extensions.DependencyInjection;
using Goke.Core;
using System.Text.Encodings.Web;
using Goke.Web.Identity;
using Goke.Web.Data;
using Goke.Web.Data.Models;

namespace Goke.Web.ServerUI.Data;

public class SeedData
{
    public static async void InitializeAsync(WebApplication app)
    {
        // seed the database
        var scope = app.Services.CreateAsyncScope();

        if (app.Environment.IsDevelopment())
        {
            await SeedData.EnsureDevelopment(scope.ServiceProvider);
        }
        else
        {
            await SeedData.EnsureProduction(scope.ServiceProvider);
        }
    }

    private static readonly IEnumerable<SeedUser> seedUsers =
    [
        new SeedUser()
        {
            Email = "manager@ark.com",
            NormalizedEmail = "MANAGER@ARK.COM",
            NormalizedUserName = "MANAGER@ARK.COM",
            RoleList = [ "Managers" ],
            UserName = "manager@ark.com"
        },
        new SeedUser()
        {
            Email = "officer@ark.com",
            NormalizedEmail = "OFFICER@ARK.COM",
            NormalizedUserName = "OFFICER@ARK.COM",
            RoleList = [ "Officers" ],
            UserName = "officer@ark.com"
        },
        new SeedUser()
        {
            Email = "user@ark.com",
            NormalizedEmail = "USER@ARK.COM",
            NormalizedUserName = "USER@ARK.COM",
            RoleList = [ "Users" ],
            UserName = "user@ark.com"
        },
    ];

    private static readonly IEnumerable<SeedUser> adminUsers =
    [
        new SeedUser()
        {
            Email = "admin@goke.me",
            NormalizedEmail = "ADMIN@GOKE.ME",
            NormalizedUserName = "GOKE@ARK.COM",
            RoleList = ["SystemAdministrators", "Administrators", "Managers" ],
            UserName = "goke@ark.com",
            Password = "goke@ARK#246800",
        },
        new SeedUser()
        {
            Email = "sysadmin@ark.com",
            NormalizedEmail = "SYSADMIN@ARK.COM",
            NormalizedUserName = "SYSADMIN@ARK.COM",
            RoleList = ["SystemAdministrators", "Administrators", "Managers"],
            UserName = "sysadmin@ark.com",
            Password = "sysadmin@ARK#789",
        },
        new SeedUser()
        {
            Email = "admin@ark.com",
            NormalizedEmail = "ADMIN@ARK.COM",
            NormalizedUserName = "ADMIN@ARK.COM",
            RoleList = ["Administrators", "Managers"],
            UserName = "admin@ark.com",
            Password = "admin@ARK#135",
        },
    ];


    public static async Task EnsureDevelopment(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        
        context.Database.EnsureDeleted();
        context.Database.Migrate();

        using RoleManager<IdentityRole> roleManager = await EnsureRoles(serviceProvider);

        var userStore = new UserStore<ApplicationUser>(context);
        var hasher = new PasswordHasher<ApplicationUser>();

        using var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        foreach (var user in adminUsers.Union(seedUsers))
        {
            var hashed = hasher.HashPassword(user, "Passw0rd!");
            user.PasswordHash = hashed;

            user.EmailConfirmed = true;

            //var result = await userStore.CreateAsync(user);
            var result = await userManager.CreateAsync(user, "Passw0rd!");

            if (user.Email is not null)
            {
                var appUser = await userManager.FindByEmailAsync(user.Email);

                if (appUser is not null && user.RoleList is not null)
                {
                    await userManager.AddToRolesAsync(appUser, user.RoleList);
                }
            }
        }
        await context.SaveChangesAsync();
    }


    public static async Task EnsureProduction(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        context.Database.Migrate();

        using RoleManager<IdentityRole> roleManager = await EnsureRoles(serviceProvider);


        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        using var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        await EnsureAdmins(context, configuration, userManager);

        await context.SaveChangesAsync();
    }

    private static async Task EnsureAdmins(ApplicationDbContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager, IEmailSender<ApplicationUser>? emailSender = null)
    {
        // var userStore = new UserStore<ApplicationUser>(context);
        // var hasher = new PasswordHasher<ApplicationUser>();
        foreach (var user in adminUsers)
        {

            var password = emailSender is not null ? 
                Text.GeneratePin() : 
                configuration.GetValue<string>($"{user.UserName!}:Secret") ?? user.Password;
            if (password != null)
            {
                //var hashed = hasher.HashPassword(user, password);
                //user.PasswordHash = hashed;

                user.EmailConfirmed = true;

                //await userStore.CreateAsync(user);
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

    public static async Task EnsureResetAdmins(ApplicationDbContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager, IEmailSender<ApplicationUser>? emailSender = null)
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

        await EnsureAdmins(context, configuration, userManager, emailSender);
    }

    private static async Task<RoleManager<IdentityRole>> EnsureRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = ["SystemAdministrators", "Administrators", "Managers", "Officers", "Users"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        return roleManager;
    }


    private class SeedUser : ApplicationUser
    {
        public string[]? RoleList { get; set; }
        public string? Password { get; set; }
    }
}