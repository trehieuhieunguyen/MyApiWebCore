using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;

namespace MyApiWebCore.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private ProductStoreContext context;
        
        private IMapper mapper;

        public CartItemRepository(
            ProductStoreContext context,
            
            IMapper mapper)
        {
            this.context = context;
          
            this.mapper = mapper;
        }
        public async Task<int> AddCartItemsAsyn(CartItemModel orderDetailModel)
        {
            var cartDetail = mapper.Map<CartItem>(orderDetailModel);
            cartDetail.CreatedAt = DateTime.Now;
            context.CartItem!.Add(cartDetail);
            await context.SaveChangesAsync();
            return cartDetail.CartId;
        }

        public async Task DeleteCartDetailAsyn(int id)
        {
            var cartDetail = context.CartItem!.FirstOrDefault(find => find.Id == id);
            if (cartDetail != null)
            {
                context.CartItem!.Remove(cartDetail);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<CartItemModel>> GetAllCartDetailAsync()
        {
            var listCartItem = await context.CartItem!.ToListAsync();
            return mapper.Map<List<CartItemModel>>(listCartItem);
        }

        public async Task<CartItemModel> GetCartDetailAsyn(int id)
        {
            var cartItem = await context.CartItem!.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<CartItemModel>(cartItem);
        }

        public async Task UpdateCartDetailAsyn(int id, CartItemModel cartItemModel)
        {
            if (id == cartItemModel.Id)
            {
                var cartItem = mapper.Map<CartItem>(cartItemModel);
                cartItem.UpdatedAt = DateTime.Now;
                context.CartItem!.Update(cartItem);
                await context.SaveChangesAsync();
            }
        }
    }
}
