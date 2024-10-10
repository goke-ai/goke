
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Goke.Web.ServerUI.Models;
using Goke.Web.ServerUI.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Goke.Web.ServerUI.Data;

public class SeedData
{

    public static async void InitializeAsync(WebApplication app)
    {
        // seed the database
        var scope = app.Services.CreateAsyncScope();

        if (app.Environment.IsDevelopment())
        {
            await SeedData.Development(scope.ServiceProvider);
        }
        else
        {
            await SeedData.Production(scope.ServiceProvider);
        }
    }

    private static readonly IEnumerable<SeedUser> seedUsers =
    [
        new SeedUser()
        {
            Email = "sysadmin@ark.com",
            NormalizedEmail = "SYSADMIN@ARK.COM",
            NormalizedUserName = "SYSADMIN@ARK.COM",
            RoleList = ["SystemAdministrators", "Administrators", "Managers" ],
            UserName = "sysadmin@ark.com"
        },
        new SeedUser()
        {
            Email = "admin@ark.com",
            NormalizedEmail = "ADMIN@ARK.COM",
            NormalizedUserName = "ADMIN@ARK.COM",
            RoleList = [ "Administrators", "Managers" ],
            UserName = "admin@ark.com"
        },
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
            Email = "sysadmin@ark.com",
            NormalizedEmail = "SYSADMIN@ARK.COM",
            NormalizedUserName = "SYSADMIN@ARK.COM",
            RoleList = ["SystemAdministrators", "Administrators", "Managers" ],
            UserName = "sysadmin@ark.com"
        },
        new SeedUser()
        {
            Email = "admin@ark.com",
            NormalizedEmail = "ADMIN@ARK.COM",
            NormalizedUserName = "ADMIN@ARK.COM",
            RoleList = [ "Administrators", "Managers" ],
            UserName = "admin@ark.com"
        },
    ];


    public static async Task Development(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        
        context.Database.EnsureDeleted();
        context.Database.Migrate();

        using RoleManager<IdentityRole> roleManager = await SeedRoles(serviceProvider);

        var userStore = new UserStore<ApplicationUser>(context);
        var password = new PasswordHasher<ApplicationUser>();

        using var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        foreach (var user in seedUsers)
        {
            var hashed = password.HashPassword(user, "Passw0rd!");
            user.PasswordHash = hashed;

            user.EmailConfirmed = true;

            var result = await userStore.CreateAsync(user);

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


    public static async Task Production(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        context.Database.Migrate();

        using RoleManager<IdentityRole> roleManager = await SeedRoles(serviceProvider);
        
        var userStore = new UserStore<ApplicationUser>(context);
        var password = new PasswordHasher<ApplicationUser>();

        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        using var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        foreach (var user in seedUsers)
        {
            var pwd = configuration.GetValue<string>(user.UserName!);
            if (pwd != null)
            {
                var hashed = password.HashPassword(user, pwd);
                user.PasswordHash = hashed;

                user.EmailConfirmed = true;

                var result = await userStore.CreateAsync(user);

                if (user.Email is not null)
                {
                    var appUser = await userManager.FindByEmailAsync(user.Email);

                    if (appUser is not null && user.RoleList is not null)
                    {
                        await userManager.AddToRolesAsync(appUser, user.RoleList);
                    }
                }
            }
        }

        await context.SaveChangesAsync();
    }

    private static async Task<RoleManager<IdentityRole>> SeedRoles(IServiceProvider serviceProvider)
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
    }
}