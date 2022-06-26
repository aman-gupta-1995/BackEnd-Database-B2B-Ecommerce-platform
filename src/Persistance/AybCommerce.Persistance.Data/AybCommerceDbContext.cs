using AybCommerce.Domain.Base;
using AybCommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AybCommerce.Persistance.Data 
{
    public class AybCommerceDbContext : IdentityDbContext<User>
    {
        public AybCommerceDbContext(DbContextOptions<AybCommerceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region [ One to One Relationships]

            modelBuilder.Entity<User>()
                .HasOne(a => a.Address)
                .WithOne(b => b.User)
                .HasForeignKey<Address>(b => b.UserId);

            #endregion

            #region [ Many to Many Relationships]

            //modelBuilder.Entity<CategoryProduct>().HasKey(s => new { s.CategoryId, s.ProductId });

            //modelBuilder.Entity<CategoryProduct>()
            //    .HasOne<Category>(sc => sc.Category)
            //    .WithMany(s => s.CategoryProducts)
            //    .HasForeignKey(sc => sc.CategoryId);


            //modelBuilder.Entity<CategoryProduct>()
            //    .HasOne<Product>(sc => sc.Product)
            //    .WithMany(s => s.Categories)
            //    .HasForeignKey(sc => sc.ProductId);

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is AuditableEntity)
                {
                    var httpContextAccessor = this.GetService<IHttpContextAccessor>();
                    var userId = httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

                    var entity = changedEntity.Entity as AuditableEntity;
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.Created = DateTime.Now;
                        entity.CreatedId = userId ?? entity.CreatedId;
                        entity.Updated = DateTime.Now;
                        entity.CreatedId = userId ?? entity.CreatedId;
                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.Updated = DateTime.Now;
                        entity.CreatedId = userId ?? entity.CreatedId;
                    }
                }

            }
            return base.SaveChanges();
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<CartItem> CartItems { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<CategoryInfo> CategoryInfos { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        //public virtual DbSet<CategoryProduct> CategoryProduct { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        public virtual DbSet<PaymentLog> PaymentLogs { get; set; }

        public virtual DbSet<ProductInfo> ProductInfos { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<Log> Logs { get; set; }

        //public virtual DbSet<CurrentAccount> CurrentAccounts { get; set; }

        //public virtual DbSet<UserCurrentAccount> UserCurrentAccounts { get; set; }
    }
}