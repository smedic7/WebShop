using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Data;

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
                NotFound();
            }

            var category = _dbContext.Category.FirstOrDefault(c => c.Id == id);


            if(category == null)
            {
                return NotFound();
            }

            return View(category);  
        }
    
    
    
    
    }
}
