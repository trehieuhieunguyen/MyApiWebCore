using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApiWebCore.Data;
using MyApiWebCore.Repositories.IRepository;

namespace MyApiWebCore.Repositories
{
    public class HistoryOrderOfUserRepository : IHistoryOrderOfUserRepository
    {
        private ProductStoreContext context;
        private IMapper mapper;
        private UserManager<ApplicationUser> manager;

        public HistoryOrderOfUserRepository(ProductStoreContext context,
            IMapper mapper,
            UserManager<ApplicationUser> manager) {
            this.context = context;
            this.mapper = mapper;
            this.manager = manager;
        }
        public async Task<object> HistoryOrdersOfUser(string userId)
        {
            var lastOrder = context.Order!.Where(o => o.UserId == userId)
                .OrderByDescending(od => od.CreatedAt);


            if (lastOrder != null)
            {
                var orderDetail = lastOrder.Select(lastOrder => new
                {
                    lastOrder.Id,
                    lastOrder.Discount,
                    lastOrder.Total,
                    lastOrder.CreatedAt,
                    ItemOrder = lastOrder.orderDetails.Select(od => new
                    {
                        od.Id,
                        od.ProductId,
                        od.Product.Description,
                        od.Quantity,
                        od.UnitPrice,
                    }).ToList()
                });
                return orderDetail;
            }
            else
            {
                return null;
            }
        }

        public async Task<object> HistoryLatestOrder(string userId)
        {
            var lastOrder = context.Order!.Where(o => o.UserId == userId)
                 .OrderByDescending(od => od.CreatedAt);


            if (lastOrder != null)
            {
                var orderDetail = lastOrder.Select(lastOrder => new
                {
                    lastOrder.Id,
                    lastOrder.Discount,
                    lastOrder.Total,
                    lastOrder.CreatedAt,
                    ItemOrder = lastOrder.orderDetails.Select(od => new
                    {
                        od.Id,
                        od.ProductId,
                        od.Product.Description,
                        od.Quantity,
                        od.UnitPrice,
                    }).ToList()
                }).FirstOrDefault();
                return orderDetail;
            }
            else
            {
                return null;
            }
        }
    }
}
