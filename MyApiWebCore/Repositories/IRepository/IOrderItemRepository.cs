using MyApiWebCore.Data;
using MyApiWebCore.Models;

namespace MyApiWebCore.Repositories.IRepository
{
    public interface IOrderItemRepository
    {
        public Task<List<OrderItemModel>> GetAllOrderDetailAsync();

        public Task<OrderItemModel> GetOrderDetailAsyn(int id);

        public Task<int> AddOrderItemsAsyn(OrderItemModel orderDetail);

        public Task UpdateOrderDetailAsyn(int id, OrderItemModel orderDetail);

        public Task DeleteOrderDetailAsyn(int id);
    }
}
