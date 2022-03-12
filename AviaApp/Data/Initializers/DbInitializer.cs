using Data.Consts;
using Data.Entities;
using Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace Data.Initializers;

public static class DbInitializer
{
    public static async Task InitializeAsync(UserManager<AviaAppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (await roleManager.FindByNameAsync(Role.Admin) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Role.Admin));
        }

        if (await roleManager.FindByNameAsync(Role.Employee) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Role.Employee));
        }

        if (await roleManager.FindByNameAsync(Role.User) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Role.User));
        }

        if (await roleManager.FindByNameAsync(Role.Banned) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Role.Banned));
        }

        if (await userManager.FindByEmailAsync(FirstUserCreds.AdminEmail) == null)
        {
            var admin = new AviaAppUser { Email = FirstUserCreds.AdminEmail, UserName = "Admin" };
            var result = await userManager.CreateAsync(admin, FirstUserCreds.AdminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, Role.User);
                await userManager.AddToRoleAsync(admin, Role.Employee);
                await userManager.AddToRoleAsync(admin, Role.Admin);

                var token = await userManager.GenerateEmailConfirmationTokenAsync(admin);
                await userManager.ConfirmEmailAsync(admin, token);
            }
        }
    }
}