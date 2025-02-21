using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using StockLibrary.Models;
using System.Collections.ObjectModel;

namespace StockLibrary.Data.Seeders
{
    public static class SeedIdentity
    {
        public static async Task SeedIdentityRoleUser(IServiceProvider serviceProvider) 
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>;

            string[] roleNames = { "Administrateur", "IAM", "Proximité", "Invité" };

            foreach (var roleName in roleNames)
            {
                if (!roleManager.RoleExists(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }



        }
    }
}