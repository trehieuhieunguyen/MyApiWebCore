using MyApiWebCore.Data;
using MyApiWebCore.Models;

namespace MyApiWebCore.Repositories.IRepository
{
    public interface ICartRepository
    {
        public Task<List<CartModel>> GetAllCartAsync();

        public Task<CartModel> GetCartByIdAsync(int id);

        public Task<int> AddCartAsync(string userId,CartModel cart);

        public Task UpdateCartAsync(int id, CartModel cart);

        public Task DeleteCartAsync(int id);

    }
}
