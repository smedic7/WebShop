using System.ComponentModel.DataAnnotations;
using WebShop.Data;

namespace WebShop.Models
{
    public class UserRating
    {
        public int Id { get; set; }

        // Ocjena može biti u rasponu od 1 do 5
        [Range(1, 5)]
        public int Rating { get; set; }

        // Veza sa proizvodom - UserRating je za jedan proizvod
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Veza sa korisnikom - UserRating pripada jednom korisniku
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }





    }
}
