using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    public class Order
    {


        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Total price is required!")]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Total { get; set; }

        public int? Discount { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [StringLength(50)]
      
        public string BillingFirstName { get; set; }


        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(50)]

        public string BillingLastName { get; set; }

        [Required(ErrorMessage = "Email address is required!")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not vaild")]
        public string BillingEmail { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is not vaild")]
        public string BillingPhone { get; set; }

        [Required(ErrorMessage = "Address  is required!")]
        [StringLength(100)]
        public string BillingAddress { get; set; }

        [Required(ErrorMessage = "City  is required!")]
        [StringLength(50)]
        public string BillingCity { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Country  is required!")]
      
        public string BillingCountry { get; set; }

        [Required(ErrorMessage = "Postal code is required!")]
        [StringLength(20)]
        public string BillingZipCode { get; set; }  


        public string Message { get; set; }

        public string UserId { get; set; }


        [ForeignKey("OrderId")]
        public List<OrderItem> OrderItems { get; set; }









    }
}
