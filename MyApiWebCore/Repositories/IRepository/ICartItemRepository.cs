using MyApiWebCore.Models;

namespace MyApiWebCore.Repositories.IRepository
{
    public interface ICartItemRepository
    {
        public Task<List<CartItemModel>> GetAllCartDetailAsync();

        public Task<CartItemModel> GetCartDetailAsyn(int id);

        public Task<int> AddCartItemsAsyn(CartItemModel cartItem);

        public Task UpdateCartDetailAsyn(int id, CartItemModel cartItem);

        public Task DeleteCartDetailAsyn(int id);
    }
}
