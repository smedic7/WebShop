using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{


    [Area("Admin")]
    [Authorize(Roles = "Admin")]


    public class ProductController : Controller
    {

        private ApplicationDbContext _dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {


            _dbContext = dbContext;
        }



        public IActionResult Index()
        {
            return View(_dbContext.Products.ToList());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public IActionResult Create(Product product)

        {
            
            if(ModelState.IsValid)
            {
                _dbContext.Products.Add(product);   
                _dbContext.SaveChanges();

                return RedirectToAction("Index");    
                    
                    
            }
            
            
            return View(product);
            }









    }
}
