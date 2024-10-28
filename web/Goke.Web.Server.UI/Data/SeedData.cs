
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Goke.Web.Server.UI.Models;
using Goke.Web.Server.UI.Data;
using Microsoft.Extensions.DependencyInjection;
using Goke.Core;
using System.Text.Encodings.Web;
using Goke.Web.Data;
using Goke.AspNetCore.Identity;
using Goke.AspNetCore.Identity.Models;

namespace Goke.Web.Server.UI.Data;

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

    public static IEnumerable<SeedUser> AdminUsers => [
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
        

        await IdentityDevelopment(serviceProvider);

        // 
        Card card = new() { Pin = "1234Qq567890!", From = DateTime.UtcNow, To = DateTime.UtcNow.AddDays(7) };
        context.Cards.Add(card);


        await context.SaveChangesAsync();
    }

    private static async Task IdentityDevelopment(IServiceProvider serviceProvider)
    {
        using RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await IdentityManager.EnsureRoles(roleManager);

        //var userStore = new UserStore<ApplicationUser>(context);
        //var hasher = new PasswordHasher<ApplicationUser>();
        using UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        foreach (var user in AdminUsers.Union(seedUsers))
        {
            //var hashed = hasher.HashPassword(user, "Passw0rd!");
            //user.PasswordHash = hashed;

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
    }

    public static async Task EnsureProduction(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        //using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await IdentityManager.EnsureRoles(roleManager);


        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        using var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        await IdentityManager.EnsureAdmins(configuration, userManager, AdminUsers);

        await context.SaveChangesAsync();
    }

    

   
}