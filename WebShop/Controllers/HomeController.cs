using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebShop.Data;
using WebShop.Extensions;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly string _sessionKeyName = "_cart";

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

            List<Product> products = _dbContext.Products.ToList();



            if (categoryId != null)
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


            return View(products);
        }

        public IActionResult Order(List<string> errors)
        {


            List<CartItem> cartItems = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);

            if(cartItems == null)
            {
                cartItems= new List<CartItem>();
            }
        
            if(cartItems.Count == 0) 
            
            
            {
                TempData["Message"] = "You need to fill the cart in order to place an order.";
                return RedirectToAction("Index");
                



            }

            decimal sum = 0;
            ViewBag.TotalPrice = cartItems.Sum(item => sum += item.GetTotal());


            ViewBag.Errors= errors;
            
            return View(cartItems);
        
        
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {

            List<CartItem> cartItems = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);

            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
            }

            if (cartItems.Count == 0)
            {

                TempData["Message"] = "You need to fill the cart in order to place an order.";
                return RedirectToAction("Index");
            }

           
            List<string> modelErrors= new List<string>();

            if(ModelState.IsValid)
            {
                _dbContext.Order.Add(order);
                _dbContext.SaveChanges();

                int orderId = order.Id;

                foreach (var item in cartItems)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        OrderId = orderId,
                        ProductId = item.Product.Id,
                        Quantity=item.Quantity,
                        Total=item.GetTotal(),  
                    };

                    _dbContext.OrderItem.Add(orderItem);
                    _dbContext.SaveChanges();
                }


                    HttpContext.Session.SetObjectAsJson(_sessionKeyName, string.Empty);


                TempData["OrderMsg"] = "Thank you for your order!";
                   return RedirectToAction("Index");
            }


            else
            {
                foreach(var modelState in ModelState.Values)
                {
                    foreach(var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }


            }


            return RedirectToAction(nameof(Order), new { errors = modelErrors });
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
    
    
    
    
    
    }
}