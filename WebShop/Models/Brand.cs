using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Veza sa proizvodom - Brand može imati više proizvoda
        public List<Product> Products { get; set; }




    }
}
