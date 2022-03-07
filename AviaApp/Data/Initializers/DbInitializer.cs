using Data.Consts;
using Microsoft.AspNetCore.Identity;

namespace Data.Initializers;

public static class DbInitializer
{
    public static async Task InitializeAsync(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (await roleManager.FindByNameAsync(FirstUserCreds.AdminRole) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(FirstUserCreds.AdminRole));
        }

        if (await roleManager.FindByNameAsync(FirstUserCreds.ModeratorRole) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(FirstUserCreds.ModeratorRole));
        }

        if (await roleManager.FindByNameAsync(FirstUserCreds.UserRole) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(FirstUserCreds.UserRole));
        }

        if (await userManager.FindByEmailAsync(FirstUserCreds.AdminEmail) == null)
        {
            var admin = new IdentityUser { Email = FirstUserCreds.AdminEmail, UserName = "Admin" };
            var result = await userManager.CreateAsync(admin, FirstUserCreds.AdminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, FirstUserCreds.AdminRole);

                var token = await userManager.GenerateEmailConfirmationTokenAsync(admin);
                await userManager.ConfirmEmailAsync(admin, token);
            }
        }
    }
}