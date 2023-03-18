using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;

namespace MyApiWebCore.Repositories
{
    public class TransactionHistoryRepository : ITransactionHistoryRepositrory
    {
        private IOrderItemRepository itemRepository;
        private IOrderRepository orderRepository;
        private UserManager<ApplicationUser> userManager;
        private ProductStoreContext context;
        private IMapper mapper;

        public TransactionHistoryRepository(IOrderItemRepository itemRepository, IOrderRepository orderRepository,
            UserManager<ApplicationUser> userManager, ProductStoreContext context,
            IMapper mapper) {
            this.itemRepository = itemRepository;
            this.orderRepository = orderRepository;
            this.userManager = userManager;
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<object> TransactionHistory(string userId,List<OrderItemModel> orderDetails)
        {
            //Create ordermodel to add order first in db 
            OrderModel orderModel = new OrderModel();
            orderModel.UserId = userId;
            orderModel.CreatedAt = DateTime.Now;
            var IdOrder = await orderRepository.AddOrderAsync(orderModel);
            orderModel.Total = 0;
            //Caculate total of bill and add orderitem of order
            for (int i = 0; i < orderDetails.Count; i++)
            {
                OrderItemModel newOrderDetail = new OrderItemModel();
                newOrderDetail = orderDetails[i];
                newOrderDetail.OrderId = IdOrder;
                orderModel.Total += newOrderDetail.UnitPrice * newOrderDetail.Quantity;
                newOrderDetail.CreatedAt = DateTime.Now;
                context.OrderDetail!.Add(mapper.Map<OrderDetail>(newOrderDetail));
            }
            //**Get order to update (not using id above to update)
            var orderUpdate = context.Order!.First<Order>(x => x.Id == IdOrder);
            orderUpdate.Total = orderModel.Total;
            context.Update<Order>(mapper.Map<Order>(orderUpdate));
            await context.SaveChangesAsync();
            //UpdateInventory
            await orderRepository.UpdateInventory(orderUpdate);
            var orders = context.Order!.Where(u => u.Id == orderUpdate.Id)
                .Select(o => new
                {
                    o.Id,
                    o.User.FirstName,
                    o.User.LastName,
                    o.Total,
                    OrderItem = o.orderDetails.Select(oi => new
                    {
                        oi.ProductId,
                        oi.Product.Description,
                        oi.Quantity,
                        oi.UnitPrice,
                    }).ToList()
                }
                ).ToList();
            
            return orders;
        }
    }
}
