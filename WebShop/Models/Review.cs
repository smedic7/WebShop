using System.ComponentModel.DataAnnotations;
using WebShop.Data;

namespace WebShop.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Comment { get; set; }

        // Ocjena može biti u rasponu od 1 do 5
        [Range(1, 5)]
        public int Rating { get; set; }

        // Veza sa proizvodom - Review pripada jednom proizvodu
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Veza sa korisnikom - Review je ostavio jedan korisnik
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }






    }
}
