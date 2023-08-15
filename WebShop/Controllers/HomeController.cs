using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private ApplicationDbContext _dbContext;



        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }



        [Authorize]

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Product(int? categoryId)
        {

            List<Product> products = new List<Product>();



            if(categoryId != null)
            {
                products = (from product in _dbContext.Products
                            join pCat in _dbContext.ProductCategory on product.Id equals pCat.ProductId
                            where pCat.CategoryId == categoryId

                            select new Product
                            {
                                Id = product.Id,
                                Title = product.Title,
                                Description = product.Description,
                                Quantity = product.Quantity,
                                Price = product.Price,
                            }).ToList();   


            }




            ViewBag.Categories = _dbContext.Category.Select(c => new SelectListItem()
            {

                Value = c.Id.ToString(),
                Text = c.Title



            }).ToList();


            return View(_dbContext.Products.ToList());
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}