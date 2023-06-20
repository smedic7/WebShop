using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {



        }
    }

        public class ApplicationUser :IdentityUser
        {
            [StringLength(50)]
            public string FirstName { get;set; }
            [StringLength(50)]
            public string LastName { get;set; }
            [StringLength(150)]
            public string Address { get;set; }



        }






}