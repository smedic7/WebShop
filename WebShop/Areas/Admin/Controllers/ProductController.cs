using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebShop.Areas.Admin.Controllers
{


    [Area("Admin")]
    [Authorize(Roles = "Admin")]


    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
