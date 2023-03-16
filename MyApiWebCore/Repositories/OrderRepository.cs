using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;

namespace MyApiWebCore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private ProductStoreContext context;
        private IMapper mapper;

        public OrderRepository(ProductStoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddOrderAsync(OrderModel orderModel)
        {
            var order = mapper.Map<Order>(orderModel);
            order.CreatedAt = DateTime.Now;
            context.Order!.Add(order);
            await context.SaveChangesAsync();
            return order.Id;
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = context.Order!.FirstOrDefault(x => x.Id == id);
            if (order != null)
            {
                context.Order!.Remove(order);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<OrderModel>> GetAllOrderAsync()
        {
            var listOrder = await context.Order!.ToListAsync();
            return mapper.Map<List<OrderModel>>(listOrder);
        }

        public async Task<OrderModel> GetOrderByIdAsync(int id)
        {
            var order = await context.Order!.FirstOrDefaultAsync(x=> x.Id == id);
            return mapper.Map<OrderModel>(order);
        }

        public async Task UpdateOrderAsync(int id, OrderModel order)
        {
            if(order.Id == id)
            {
                var orderUpdate = mapper.Map<Order>(order);
                orderUpdate.UpdatedAt = DateTime.Now;
                context.Order!.Update(orderUpdate);
                await context.SaveChangesAsync();
            }
        }
        public async Task UpdateInventory(Order order)
        {
            foreach (var item in order.orderDetails)
            {
                var product = context.Products!.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    product.Quantity -= item.Quantity;
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
