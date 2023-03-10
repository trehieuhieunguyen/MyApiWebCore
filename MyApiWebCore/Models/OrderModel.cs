using MyApiWebCore.Data;

namespace MyApiWebCore.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public float Discount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        public double Total { get; set; }
    }
}
