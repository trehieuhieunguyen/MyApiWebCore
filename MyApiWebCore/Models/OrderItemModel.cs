using MyApiWebCore.Data;

namespace MyApiWebCore.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
