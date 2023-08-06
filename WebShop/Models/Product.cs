﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    public class Product
    {

        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength =2)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,2)")]

        public decimal Price { get; set; }

        [ForeignKey("ProductId")]
        public List<ProductCategory>? ProductsCategories { get; set;}

        [ForeignKey("ProductId")]
        public List<OrderItem>? OrderItems { get; set; }






    }
}
