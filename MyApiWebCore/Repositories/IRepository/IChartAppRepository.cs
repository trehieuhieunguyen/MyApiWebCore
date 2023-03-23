using MyApiWebCore.Models.Dto;

namespace MyApiWebCore.Repositories.IRepository
{
    public interface IChartAppRepository
    {
        public Task<object> GetProductChartDataByMonth(int year);
    }
}
