﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiWebCore.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Range(0,double.MaxValue)]
        public double Price { get; set; }

        [Range(0,1000)]
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
