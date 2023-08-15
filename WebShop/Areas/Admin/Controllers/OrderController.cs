using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebShop.Data;

namespace WebShop.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class OrderController : Controller
    {

        private ApplicationDbContext _dbContext;

            public OrderController(ApplicationDbContext dbContext)
            {
                _dbContext=dbContext;

            }

        public IActionResult Index()
        {
            return View(_dbContext.Order.ToList());
        }
    }
}
