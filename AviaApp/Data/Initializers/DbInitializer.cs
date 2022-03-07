using Data.Consts;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data.Initializers;

public static class DbInitializer
{
    public static async Task InitializeAsync(UserManager<AviaAppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (await roleManager.FindByNameAsync(FirstUserCreds.AdminRole) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(FirstUserCreds.AdminRole));
        }

        if (await roleManager.FindByNameAsync(FirstUserCreds.EmployeeRole) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(FirstUserCreds.EmployeeRole));
        }

        if (await roleManager.FindByNameAsync(FirstUserCreds.UserRole) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(FirstUserCreds.UserRole));
        }

        if (await userManager.FindByEmailAsync(FirstUserCreds.AdminEmail) == null)
        {
            var admin = new AviaAppUser { Email = FirstUserCreds.AdminEmail, UserName = "Admin" };
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