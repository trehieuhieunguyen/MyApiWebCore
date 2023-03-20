using MyApiWebCore.Data;
using MyApiWebCore.Models;

namespace MyApiWebCore.Repositories.IRepository
{
    public interface IProductRepository
    {
        public Task<List<ProductModel>> GetAllProductAsync();

        public Task<ProductModel> GetProductAsync(int id);

        public Task<int> AddProductAsync(ProductModel model);

        public Task UpdateProductAsync(int id, ProductModel model);

        public Task DeleteProductAsync(int id);

        public Task<IEnumerable<Product>> GetProductFilter(int page, int pageSize, string filter);
    }
}
