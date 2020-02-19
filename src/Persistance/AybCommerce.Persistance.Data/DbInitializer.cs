using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AybCommerce.Domain.Entities;
using AybCommerce.Domain.Enumerations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AybCommerce.Persistance.Data
{
    public class DbInitializer
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AybCommerceDbContext context)
        {
            if (!context.Users.Any() && !context.Roles.Any())
            {
                await CreateUsersAndRoles(userManager, roleManager);
            }

            var adminId = context.Users.FirstOrDefault(x => x.UserName == "admin@admin.com")?.Id;
            if (!context.Categories.Any())
            {
                CreateCategories(context, adminId);
            }

            if (!context.Products.Any())
            {
                CreateProducts(context, adminId);
            }


        }


        private static void CreateProducts(AybCommerceDbContext context, string adminId)
        {
            var products = new List<Product>
            {
                #region Camera

                new Product {CreatedId = adminId, CategoryId = 1, Name = "GoPro HERO7 Black", Barcode="AybC001", ErpCode="AybC001",Price= 100m, SalePrice=80m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 1, Name = "Fujifilm Instax Mini 9", Barcode="AybC002", ErpCode="AybC002",Price= 20.15m, SalePrice=8.99m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 1, Name = "Fujifilm X-T30 ", Barcode="AybC003", ErpCode="AybC003",Price= 1100m, SalePrice=840m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 1, Name = "Nikon D850", Barcode="AybC004", ErpCode="AybC004",Price= 100m, SalePrice=60.12m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 1, Name = "PANASONIC LUMIX FZ80", Barcode="AybC005", ErpCode="AybC005",Price= 50m, SalePrice=40m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 1, Name = "Sony DSCW800/B", Barcode="AybC006", ErpCode="AybC006",Price= 600m, SalePrice=400m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 1, Name = "Olympus TG-5", Barcode="AybC007", ErpCode="AybC007",Price= 700m, SalePrice=500.50m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 1, Name = "Canon PowerShot ELPH 180", Barcode="AybC008", ErpCode="AybC008",Price= 300m, SalePrice=150m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 1, Name = "Canon Digital SLR ", Barcode="AybC009", ErpCode="AybC009",Price= 188m, SalePrice=180m ,Status = EntityStatus.Active},

                #endregion

                #region Computers

                new Product {CreatedId = adminId, CategoryId = 2, Name = "Acer Aspire E 15, 15.6", Barcode="AybL001", ErpCode="AybL001",Price= 230m, SalePrice=200m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 2, Name = "ASUS VivoBook F510UA 15.6", Barcode="AybL002", ErpCode="AybL002",Price= 500m, SalePrice=450.43m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 2, Name = "Acer Aspire C24-865-ACi5NT", Barcode="AybL003", ErpCode="AybL003",Price= 600m, SalePrice=480m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 2, Name = "Huawei MateBook X Pro", Barcode="AybL004", ErpCode="AybL004",Price= 600m, SalePrice=580m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 2, Name = "Apple MacBook Air 13-inch", Barcode="AybL005", ErpCode="AybL005",Price= 1000m, SalePrice=800m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 2, Name = "Microsoft Surface Laptop 2", Barcode="AybL006", ErpCode="AybL006",Price= 990m, SalePrice=780m ,Status = EntityStatus.Active},

                #endregion

                #region Headphones

                new Product {CreatedId = adminId, CategoryId = 3, Name = "Sony Noise Cancelling Headphones", Barcode="AybH001", ErpCode="AybH001",Price= 160m, SalePrice=70m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 3, Name = "Mpow Flame Bluetooth Headphones", Barcode="AybH002", ErpCode="AybH002",Price= 50m, SalePrice=30m ,Status = EntityStatus.Active},
                new Product {CreatedId = adminId, CategoryId = 3, Name = "COWIN E7 Active Noise Cancelling Headphones", Barcode="AybH003", ErpCode="AybH003",Price=40m, SalePrice=10.99m ,Status = EntityStatus.Active},

                #endregion

                #region Televisions

                new Product {CreatedId = adminId, CategoryId = 4, Name = "TCL 32S327 32-Inch", Barcode="AybT001", ErpCode="AybT001",Price= 189.99m, SalePrice=149.99m ,Status = EntityStatus.Active},

                #endregion

            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void CreateCategories(AybCommerceDbContext context, string adminId)
        {
            var categories = new List<Category>
            {
                new Category {Name = "Camera", Status = EntityStatus.Active, CreatedId = adminId},
                new Category {Name = "Computers", Status = EntityStatus.Active, CreatedId = adminId},
                new Category {Name = "Headphones", Status = EntityStatus.Active, CreatedId = adminId},
                new Category {Name = "Televisions", Status = EntityStatus.Active, CreatedId = adminId}
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static async Task CreateUsersAndRoles(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminUser = new User
            {
                Name = "Admin",
                Surname = "Admin",
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                Status = EntityStatus.Active,
            };
            await userManager.CreateAsync(adminUser, "123123");
            var aybUser = new User
            {
                Name = "Ayb",
                Surname = "Ars",
                UserName = "ayb@ars.com",
                Email = "ayb@ars.com",
                Status = EntityStatus.Active,
            };
            await userManager.CreateAsync(aybUser, "123123");
            var berkUser = new User
            {
                Name = "Berk",
                Surname = "Cerdik",
                UserName = "berk@cerdik.com",
                Email = "berk@cerdik.com",
                Status = EntityStatus.Active,
            };
            await userManager.CreateAsync(berkUser, "123123");

            var adminRole = new IdentityRole { Name = "Administrator" };
            await roleManager.CreateAsync(adminRole);
            var cutromerRole = new IdentityRole { Name = "Customer" };
            await roleManager.CreateAsync(cutromerRole);

            await userManager.AddToRoleAsync(adminUser, "Administrator");
            await userManager.AddToRoleAsync(aybUser, "Administrator");
            await userManager.AddToRoleAsync(berkUser, "Customer");
        }
    }
}
