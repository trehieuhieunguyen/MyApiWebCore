using System.ComponentModel.DataAnnotations;

namespace MyApiWebCore.Data
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public float Discount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        public double Total { get; set; }
    }
}
