using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "Adminadmin123!";
        private const string adminEmail = "admin@gmail.com";
        private const string adminPhone = "801-555-3333";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            IdentityDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<IdentityDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminUser);



            if (user == null)
            {
                user = new IdentityUser(adminUser);
                user.Email = adminEmail;
                user.PhoneNumber = adminPhone;
                var results = await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
