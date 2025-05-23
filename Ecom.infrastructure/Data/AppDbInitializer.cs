using Ecom.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Ecom.infrastructure.Data
{
    public static class AppDbInitializer
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            var admin = new AppUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com"
            };

            if (userManager.Users.All(u => u.UserName != admin.UserName))
            {
                var result = await userManager.CreateAsync(admin, "admin");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
