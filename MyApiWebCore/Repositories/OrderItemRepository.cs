using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;

namespace MyApiWebCore.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private ProductStoreContext context;
        private IMapper mapper;

        public OrderItemRepository(ProductStoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddOrderItemsAsyn(OrderItemModel orderItemModel)
        {
            var orderDetail = mapper.Map<OrderDetail>(orderItemModel);
            context.OrderDetail!.Add(orderDetail); 
            await context.SaveChangesAsync();
            return orderDetail.OrderId;
        }

        public async Task DeleteOrderDetailAsyn(int id)
        {
            var OrderDetail = context.OrderDetail!.FirstOrDefault(find => find.Id == id);
            if (OrderDetail != null)
            {
                context.OrderDetail!.Remove(OrderDetail);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<OrderItemModel>> GetAllOrderDetailAsync()
        {
            var listOrderItem = await context.OrderDetail!.ToListAsync();
            return mapper.Map<List<OrderItemModel>>(listOrderItem);
        }

        public async Task<OrderItemModel> GetOrderDetailAsyn(int id)
        {
            var orderItem = await context.OrderDetail!.FirstOrDefaultAsync(x=>x.Id==id);
            return mapper.Map<OrderItemModel>(orderItem);
        }

        public async Task UpdateOrderDetailAsyn(int id, OrderItemModel orderItemModel)
        {
            if(id == orderItemModel.Id)
            {
                var orderItem = mapper.Map<OrderDetail>(orderItemModel);
                context.OrderDetail!.Update(orderItem);
                await context.SaveChangesAsync();
            }
        }
    }
}
