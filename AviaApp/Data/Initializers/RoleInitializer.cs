using Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace Data.Initializers;

public static class RoleInitializer
{
    public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
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

        if (await roleManager.FindByNameAsync(Role.AutoJob) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Role.AutoJob));
        }
    } 
}