using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{


    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {

        private ApplicationDbContext _dbContext;


        public CategoryController(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return View(_dbContext.Category.ToList());
        }
    
    
        public IActionResult Details(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }

            var category = _dbContext.Category.FirstOrDefault(c => c.Id == id);


            if(category == null)
            {
                return NotFound();
            }

            return View(category);  
        }
    
    
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost] 
        public IActionResult Create(Category category)
        {

            if(ModelState.IsValid) 
            
            {

                _dbContext.Category.Add(category);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);

          

        }

            public IActionResult Edit(int id)
            {

            if (id == 0)
            {
                return NotFound();
            }

                var category = _dbContext.Category.FirstOrDefault(c=>c.Id == id);

                if(category == null)
            {
                return NotFound();
            }

            return View(category);
            }


            [HttpPost]
            public IActionResult Edit(Category category)
            {
            if (ModelState.IsValid)
            {
                _dbContext.Update(category);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
                return View(category);
            }



    }
}
