using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiWebCore.Data;
using MyApiWebCore.Models.Dto;
using MyApiWebCore.Repositories.IRepository;
using System.Linq;

namespace MyApiWebCore.Repositories
{
    public class ChartAppRepository : IChartAppRepository
    {
        private ProductStoreContext context;

        public ChartAppRepository(ProductStoreContext context) {
            this.context = context;
        }
        public async Task<object> GetProductChartDataByMonth(int year)
        {
            var result = from orderDetail in context.OrderDetail
                         join order in context.Order on orderDetail.OrderId equals order.Id
                         join products in context.Products on orderDetail.ProductId equals products.Id
                         where order.CreatedAt.Year == year && order.CreatedAt.Month >= 1 && order.CreatedAt.Month <= 12
                         group orderDetail by new { products.Id, products.Description } into g
                         select new
                         {
                             ProductId = g.Key.Id,
                             ProductName = g.Key.Description,
                             

                             Month1 =new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 1).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 1).Sum(x => x.Quantity*x.UnitPrice)
                             },
                             Month2 = new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 2).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 2).Sum(x => x.Quantity * x.UnitPrice)
                             },
                             Month3 = new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 3).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 3).Sum(x => x.Quantity * x.UnitPrice)
                             },
                             Month4 = new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 4).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 4).Sum(x => x.Quantity * x.UnitPrice)
                             },
                             Month5 = new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 5).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 5).Sum(x => x.Quantity * x.UnitPrice)
                             },
                             Month6 = new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 6).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 6).Sum(x => x.Quantity * x.UnitPrice)
                             },
                             Month7 = new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 7).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 7).Sum(x => x.Quantity * x.UnitPrice)
                             },
                             Month8 = new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 8).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 8).Sum(x => x.Quantity * x.UnitPrice)
                             },
                             Month9 = new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 9).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 9).Sum(x => x.Quantity * x.UnitPrice)
                             },
                             Month10 = new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 10).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 10).Sum(x => x.Quantity * x.UnitPrice)
                             },
                             Month11 = new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 11).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 11).Sum(x => x.Quantity * x.UnitPrice)
                             },
                             Month12 = new
                             {
                                 QuatitiesSold = g.Where(x => x.Order.CreatedAt.Month == 12).Sum(x => x.Quantity),
                                 Revenues = g.Where(x => x.Order.CreatedAt.Month == 12).Sum(x => x.Quantity * x.UnitPrice)
                             },
                         };
           
            return result;
        }
    }
}
