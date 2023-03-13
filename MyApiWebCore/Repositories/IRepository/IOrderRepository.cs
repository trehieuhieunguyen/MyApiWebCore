using MyApiWebCore.Data;
using MyApiWebCore.Models;

namespace MyApiWebCore.Repositories.IRepository
{
    public interface IOrderRepository
    {
        public Task<List<OrderModel>> GetAllOrderAsync();

        public Task<OrderModel> GetOrderByIdAsync(int id);

        public Task<int> AddOrderAsync(OrderModel order);

        public Task UpdateOrderAsync(int id, OrderModel order);

        public Task DeleteOrderAsync(int id);
    }
}
