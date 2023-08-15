using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Runtime.CompilerServices;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{

    [Area("Admin"), Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {

        private ApplicationDbContext _dbContext;


        public ProductCategoryController(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
        }


        public IActionResult Index(int productId)
        {


            var productCategoryList = _dbContext.ProductCategory.Where(pc => pc.ProductId == productId)
                .Select(pc => new ProductCategory()
                {
                    Id = pc.ProductId,
                    ProductId = pc.ProductId,
                    CategoryId = pc.CategoryId,
                    ProductName = _dbContext.Products.SingleOrDefault(y => y.Id == pc.ProductId).Title,
                    CategoryName = _dbContext.Category.SingleOrDefault(y => y.Id == pc.CategoryId).Title,
                });


            ViewBag.ProductId = productId;


            return View(productCategoryList);
        }



        public IActionResult Create(int productId)

        {

            ViewBag.ProductId = productId;

            ViewBag.Categories = _dbContext.Category.Select(



                c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title,
                }).ToList();



            return View();



        }


        [HttpPost]


        public IActionResult Create(ProductCategory productCategory)
        {

            if (ModelState.IsValid)
            {

                _dbContext.ProductCategory.Add(productCategory);
                _dbContext.SaveChanges();

                return RedirectToAction("Index", new { productId = productCategory.ProductId });

            }

            return View(productCategory);
        }


        public IActionResult Details(int id)
        {

            if (id == 0) return NotFound();

            var productCategory = _dbContext.ProductCategory
                .SingleOrDefault(pc => pc.Id == id);

            productCategory.ProductName = _dbContext.Products

                    .SingleOrDefault(x => x.Id == productCategory.ProductId).Title;
            productCategory.CategoryName = _dbContext
             .Category
             .SingleOrDefault(x => x.Id == productCategory.CategoryId).Title;


            if (productCategory == null) return NotFound();

            return View(productCategory);

            return View(productCategory);
        }


        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();

            var productCategory = _dbContext.ProductCategory.FirstOrDefault(p => p.Id == id);

            if (productCategory == null) return NotFound();


            return View(productCategory);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id == 0) return NotFound();
            var productCategory = _dbContext.ProductCategory.FirstOrDefault(p => p.Id == id);

            if (productCategory == null) return NotFound();

            _dbContext.ProductCategory.Remove(productCategory);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index), new { productId = productCategory.ProductId });
        }




        public IActionResult Edit(int id, int productId)
        {
            Console.WriteLine("Edit method called.");

            if (id == 0)
            {
                Console.WriteLine("Invalid id provided.");
                return NotFound();
            }
            
            var productCategory= _dbContext.ProductCategory.FirstOrDefault(x => x.Id == id);
           

            if (productCategory == null)
            {
                Console.WriteLine("ProductCategory not found.");
                return NotFound();
            }

            ViewBag.ProductId = productId;
                
            ViewBag.Categories = _dbContext.Category.Select

                (
                 c => new SelectListItem()
                 {
                     Value = c.Id.ToString(),
                     Text = c.Title
                 }
                ).ToList();

            ViewBag.Products = _dbContext.Products.Select
                (
                    p => new SelectListItem()
                    {
                        Value = p.Id.ToString(),
                        Text = p.Title
                    }
                ).ToList();

            return View(productCategory);
        }

        [HttpPost]
        public IActionResult Edit(ProductCategory productCategory)
        {
            Console.WriteLine("Edit POST method called.");

            if (productCategory == null || productCategory.Id == 0)
            {
                Console.WriteLine("Invalid ProductCategory provided.");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.ProductCategory.Update(productCategory);
                _dbContext.SaveChanges();

                Console.WriteLine("ProductCategory updated successfully.");
                return RedirectToAction(nameof(Index), new { productId = productCategory.ProductId });
            }

            Console.WriteLine("Invalid ModelState.");
            return View(productCategory);
        }







    }




}


