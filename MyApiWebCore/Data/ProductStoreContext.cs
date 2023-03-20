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
        public DbSet<Cart>? Cart { get; set; }
        public DbSet<CartItem>? CartItem { get; set;}
        public DbSet<RefreshToken> RefreshToken { get; set; }
    }
}
