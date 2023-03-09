using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyApiWebCore.Data
{
    public class ProductStoreContext : IdentityDbContext<ApplicationUser>
    {
        public ProductStoreContext(DbContextOptions<ProductStoreContext> options) : base(options) { }

        #region DbSet
        public DbSet<Product>? Products { get; set;}
        #endregion
        public DbSet<Order>? Order { get; set; }
        public DbSet<OrderDetail>? OrderDetail { get; set; }

    }
}
