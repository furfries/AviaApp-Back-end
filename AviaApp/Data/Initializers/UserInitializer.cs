using Data.Consts;
using Data.Entities;
using Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace Data.Initializers;

public static class UserInitializer
{
    public static async Task InitializeAsync(UserManager<AviaAppUser> userManager)
    {
        if (await userManager.FindByEmailAsync(FirstUserCreds.AdminEmail) == null)
        {
            var admin = new AviaAppUser { Email = FirstUserCreds.AdminEmail, UserName = FirstUserCreds.AdminEmail };
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
        if (await userManager.FindByEmailAsync(FirstUserCreds.AutoJobEmail) == null)
        {
            var autoJobUser = new AviaAppUser { Email = FirstUserCreds.AutoJobEmail, UserName = FirstUserCreds.AutoJobEmail };
            var result = await userManager.CreateAsync(autoJobUser, FirstUserCreds.AutoJobPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(autoJobUser, Role.Admin);
                await userManager.AddToRoleAsync(autoJobUser, Role.AutoJob);

                var token = await userManager.GenerateEmailConfirmationTokenAsync(autoJobUser);
                await userManager.ConfirmEmailAsync(autoJobUser, token);
            }
        }
    }
}