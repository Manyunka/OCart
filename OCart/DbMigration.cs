using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OCart.Data;
using OCart.Models;

namespace OCart
{
    public static class DbMigration
    {
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();
                ConfigureIdentity(scope).GetAwaiter().GetResult();
            }

            return webHost;
        }

        private static async Task ConfigureIdentity(IServiceScope scope)
        {
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            var artistsRole = await roleManager.FindByNameAsync(ApplicationRoles.Artists);
            if (artistsRole == null)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Artists));
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create {ApplicationRoles.Artists} role.");
                }
            }

            var customersRole = await roleManager.FindByNameAsync(ApplicationRoles.Customers);
            if (customersRole == null)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Customers));
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create {ApplicationRoles.Customers} role.");
                }
            }
        }
    }

}
