using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiWebCore.Data;
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
                             MonthlySale = g.GroupBy(x => x.Order.CreatedAt.Month).Select(d => new
                             {
                                 Month = d.Key,
                                 QuantitiesSold = d.Sum(x => x.Quantity),
                                 Revenues = d.Sum(x => x.Quantity * x.UnitPrice)
                             }).ToList()
                         };
           
            return result;
        }
    }
}
