using Microsoft.EntityFrameworkCore;

namespace MyApiWebCore.Data
{
    public class ProductStoreContext : DbContext
    {
        public ProductStoreContext(DbContextOptions<ProductStoreContext> options) : base(options) { }

        #region DbSet
        public DbSet<Product>? Products { get; set;}
        #endregion

    }
}
