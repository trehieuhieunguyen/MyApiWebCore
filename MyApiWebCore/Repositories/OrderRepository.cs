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
            context.Order!.Add(order);
            await context.SaveChangesAsync();
            return order.Id;
        }

        public Task DeleteOrderAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task UpdateOrderAsync(int id, OrderModel order)
        {
            throw new NotImplementedException();
        }
    }
}
