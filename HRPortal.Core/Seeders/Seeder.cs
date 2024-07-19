﻿
using HRPortal.Core.Context;
using HRPortal.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace HRPortal.Core.Seeders
{
    public class Seeder
    {
        public static async Task SeedData(IApplicationBuilder app)
        {

            // Get db context
            var dbContext = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<HRPortalContext>();

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }

            if (!dbContext.Roles.Any())
            {
                await dbContext.Database.EnsureCreatedAsync();
                var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<ApplicationCompany>>();
                var roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Creating list of roles
                List<string> roles = new() { "Super Admin", "Company", "HR" };

                // Creating roles
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }


            }


            // Saving everything into the database
            await dbContext.SaveChangesAsync();
        }


    }
}
