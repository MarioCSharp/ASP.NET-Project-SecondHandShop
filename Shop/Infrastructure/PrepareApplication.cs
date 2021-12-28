namespace Shop.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Shop.Data;
    using Shop.Data.Models;
    using Shop.Models;
    using System;
    using System.Linq;
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
            SeedCategories(services);
            return app;
        }
        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ShopDbContext>();
            data.Database.Migrate();
        }
        public static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<ShopDbContext>();
            if (data.Categories.Any())
            {
                return;
            }
            data.Categories.AddRange(new[]
            {
                new Category { Name = "Electronics"},
                new Category { Name = "Gaming"},
                new Category { Name = "Kid toys"},
                new Category { Name = "Board games"},
                new Category { Name = "Car parts"},
                new Category { Name = "Clothes"},
                new Category { Name = "Books"},
                new Category { Name = "School"}
            });
            data.SaveChanges();
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
