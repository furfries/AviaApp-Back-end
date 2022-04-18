using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data.Initializers;

public static class DbInitializer
{
    public static async Task InitializeAsync(UserManager<AviaAppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await RoleInitializer.InitializeAsync(roleManager);
        await UserInitializer.InitializeAsync(userManager);
    }
}