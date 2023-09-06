using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebShop.Data;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {

        public ApplicationDbContext _dbContext;

        public UserController(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;

        }


        public IActionResult Index()
        {

            var users = _dbContext.Users.ToList();



            return View(users);
        }





        public IActionResult Update(string id)
        {

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }


            return View(user);
        }


        [HttpPost]

        public IActionResult Update(string Id, string FirstName, string LastName, string Email)


        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == Id);

            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Email;
            _dbContext.Update(user);
            _dbContext.SaveChanges();


            return RedirectToAction("Index");


        }

        public IActionResult Delete(string id)
        {

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }


            return View(user);
        }



        [HttpPost]
        public IActionResult DeleteConfirm(string id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var userOrders = _dbContext.Order.Where(o => o.UserId == id).ToList();
            _dbContext.Order.RemoveRange(userOrders);

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");

        }
    }

}