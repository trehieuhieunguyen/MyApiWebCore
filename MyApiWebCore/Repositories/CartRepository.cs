using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;
using System.Security.Claims;

namespace MyApiWebCore.Repositories
{
    public class CartRepository : ICartRepository
    {
        private ProductStoreContext context;
    
        private IMapper mapper;

        public CartRepository(
            ProductStoreContext context,
           
            IMapper mapper) 
        {
            this.context = context;

            this.mapper = mapper;
        }

        public async Task<int> AddCartAsync(string userId,CartModel cartModel)
        {
            var cart = mapper.Map<Cart>(cartModel);
            cart.CreatedAt = DateTime.Now;
            cart.UserId = userId;
            context.Cart!.Add(cart);
            await context.SaveChangesAsync();
            return cart.Id;
        }

        public async Task DeleteCartAsync(int id)
        {
            var cart = context.Cart!.FirstOrDefault(x => x.Id == id);
            if (cart != null)
            {
                context.Cart!.Remove(cart);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<CartModel>> GetAllCartAsync()
        {
            var listCart = await context.Cart!.ToListAsync();
            return mapper.Map<List<CartModel>>(listCart);
        }

        public async Task<CartModel> GetCartByIdAsync(int id)
        {
            var cart = await context.Cart!.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<CartModel>(cart);
        }

        public async Task UpdateCartAsync(int id, CartModel cartModel)
        {
            if (cartModel.Id == id)
            {
                var cartUpdate = mapper.Map<Cart>(cartModel);
                cartUpdate.UpdatedAt = DateTime.Now;
                context.Cart!.Update(cartUpdate);
                await context.SaveChangesAsync();
            }
        }
    }
}
