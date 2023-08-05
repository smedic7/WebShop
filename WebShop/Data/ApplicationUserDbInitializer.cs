using Microsoft.AspNetCore.Identity;

namespace WebShop.Data
{
    public static class ApplicationUserDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "mirko@mirko.hr",
                Email = "mirko@mirko.hr"
            };

     
            var result = userManager.CreateAsync(user, "mirkomiric").Result;

        
            if (result.Succeeded)
            {
                
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }





    }
}
