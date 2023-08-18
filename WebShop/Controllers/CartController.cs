using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebShop.Data;
using WebShop.Extensions;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class CartController : Controller
    {


        private readonly string _sessionKeyName = "_cart";

        private ApplicationDbContext _dbContext;

        public CartController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {


            /*  List<CartItem> cart = new List<CartItem>();

           cart.Add(new CartItem()
           {
               Product = new Product() { Id = 1, Description = "Voće", Title = "Ananas", Price = 10 },
               Quantity = 3

           });




           HttpContext.Session.SetObjectAsJson(_sessionKeyName, cart); */




            List<CartItem> cart = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);

            if (cart == null)
            {
                cart = new List<CartItem>();
            }


            decimal sum = 0;

            cart.Sum(item => sum += item.GetTotal());
            ViewBag.TotalPrice = sum;


            return View(cart);
        }

        [HttpPost]

        public IActionResult AddToCart(int productId)
        {

            List<CartItem> cartItems = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);

            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
            }


            if (cartItems.Count == 0)
            {


                var product = _dbContext.Products.FirstOrDefault(p => p.Id == productId);

                CartItem cartItem = new CartItem()
                {
                    Product = product,
                    Quantity = 1
                };

            
                cartItems.Add(cartItem);

                HttpContext.Session.SetObjectAsJson(_sessionKeyName, cartItems);
            
            
            }


            else
            {


                int existingProductIndex = 0;

                bool productExists = false;

                for(int i = 0; i < cartItems.Count; i++)
                {

                    if (cartItems[i].Product.Id == productId)
                    {
                        productExists = true;
                        existingProductIndex = i;
                    }



                }

                if (productExists == false)
                    
                    
                {
                    var product = _dbContext.Products.FirstOrDefault(p => p.Id == productId);
                    CartItem cartItem = new CartItem()
                    {
                        Product = product,
                        Quantity = 1

                    };

                    cartItems.Add(cartItem);
                }

                else
                {


                    cartItems[existingProductIndex].Quantity++;
                }

            
                
                HttpContext.Session.SetObjectAsJson( _sessionKeyName, cartItems);
            }







            return RedirectToAction(nameof(Index));
        }

    
    
            public IActionResult RemoveFromCart(int productId)
        {

            List<CartItem> cartItems = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);

            if(cartItems == null)
            {
                cartItems = new List<CartItem>();
            }


            int productIndexToRemove = cartItems.FindIndex(item => item.Product.Id == productId);


            if (productIndexToRemove != -1)
            {
                // smanjuje količinu ako je produkt u kosarici
                if (cartItems[productIndexToRemove].Quantity > 1)
                {
                    cartItems[productIndexToRemove].Quantity--;
                }
                else
                {
                    // briše item ako postane nula iz košarice
                    cartItems.RemoveAt(productIndexToRemove);
                }

                //sejva update cart
                HttpContext.Session.SetObjectAsJson(_sessionKeyName, cartItems);
            }

            return RedirectToAction(nameof(Index));

        }
    
    
    
    
    
    }

        
    
    
    
    
    
    
    
    
    



}
