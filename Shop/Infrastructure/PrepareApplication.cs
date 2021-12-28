namespace Shop.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Shop.Data;
    using Shop.Models;
    using System;
    using System.Threading.Tasks;
    using static Constants.Administrator;
    public static class PrepareApplication
    {
        public static IApplicationBuilder PrepareApp(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
            MigrateDatabase(services);
            SeedAdministrator(services);
            return app;
        }
        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ShopDbContext>();
            data.Database.Migrate();
        }
        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }
                    var role = new IdentityRole
                    {
                        Name = AdministratorRoleName
                    };
                    await roleManager.CreateAsync(role);
                    const string adminEmail = "mario.18@abv.bg";
                    const string adminPassword = "Admin?12";
                    var user = new User
                    {
                        Email = adminEmail,
                        FullName = "Mario Petkov",
                        UserName = adminEmail
                    };
                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
