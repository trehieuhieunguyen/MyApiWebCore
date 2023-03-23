using MyApiWebCore.Data;

namespace MyApiWebCore.Repositories.IRepository
{
    public interface IHistoryOrderOfUserRepository
    {
        public Task<object> HistoryOrdersOfUser(string userId);

        public Task<object> HistoryLatestOrder(string userId);
    }
}
