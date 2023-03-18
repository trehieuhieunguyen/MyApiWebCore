using MyApiWebCore.Data;
using MyApiWebCore.Models;

namespace MyApiWebCore.Repositories.IRepository
{
    public interface ITransactionHistoryRepositrory
    {
        public Task<object> TransactionHistory(string userId,List<OrderItemModel> orderDetails);
    }
}
