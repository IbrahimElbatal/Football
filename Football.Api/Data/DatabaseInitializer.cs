using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Football.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Football.Api.Data
{
    public class DatabaseInitializer
    {
        public static async Task Seed(FootballContext context,UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            if (!context.Nationalities.Any())
            {
                context.Nationalities.AddRange(new []
                {
                    new Nationality(){Name = "Egyptian"},
                    new Nationality(){Name = "American"},
                });
                await context.SaveChangesAsync();
            }
            if (!context.Countries.Any())
            {
                context.Countries.AddRange(new[]
                {
                    new Country(){Name = "Egypt"},
                    new Country(){Name = "America"},
                });
                await context.SaveChangesAsync();
            }
            
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new Role()
                {
                    Name = "Admin"
                });

                await roleManager.CreateAsync(new Role()
                {
                    Name = "Normal"
                });
            }

            if (!userManager.Users.Any())
            {
                var adminUser = new User()
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };
                var result = await userManager.CreateAsync(adminUser, "admin_123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                var normalUser = new User()
                {
                    UserName = "normal@normal.com",
                    Email = "normal@normal.com"
                };
                var result2 = await userManager.CreateAsync(normalUser, "normal_123");
                if (result2.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "Normal");
                }

            }
        }
    }
}
