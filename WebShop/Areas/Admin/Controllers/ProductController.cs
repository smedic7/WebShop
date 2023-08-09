using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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



        public IActionResult Details(int id)
        {
            if(id == 0)
            {

                return NotFound();
            }


            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }
           

            return View(product);
            
            
            
            


        }







        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public IActionResult Create(Product product)

        {

            if (ModelState.IsValid)
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");


            }


            return View(product);
        }
         

            
        public IActionResult Edit(int id) 
        {
            if (id == 0)
            {
                return NotFound();
            }

            var product= _dbContext.Products.FirstOrDefault(p => p.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]



        public IActionResult Edit(Product product) 
        {
            if(product == null || product.Id ==0)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                _dbContext.Products.Update(product);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
         
            return View(product);
            
        }
        
        

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
        
            var product = _dbContext.Products.FirstOrDefault(p => p.Id==id);

            if (product == null)
            {

                return NotFound();
            }
        
        
        
        
            return View(product);
        }

        [HttpPost]


        public IActionResult DeleteConfirm(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }


            var product = _dbContext.Products.FirstOrDefault(p =>p.Id==id);

            
            if(product == null)
            {

                return NotFound();  
            }
            
            
            _dbContext.Remove(product);
            _dbContext.SaveChanges();


            return RedirectToAction("Index");
        }
      
        


    }
}
