using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebShop.Extensions;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class CartController : Controller
    {


        private readonly string _sessionKeyName = "_cart";
        public IActionResult Index()
        {

          /*  List<CartItem> cart = new List<CartItem>();

            cart.Add(new CartItem()
            {
                Product = new Product() { Id = 1, Description = "Voće", Title = "Ananas", Price = 10 },
                Quantity = 3

            });




            HttpContext.Session.SetObjectAsJson(_sessionKeyName, cart); */



           


            List<CartItem> cart=HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);

            if(cart == null)
            {
                cart= new List<CartItem>();
            }
            

            decimal sum = 0;

            cart.Sum(item => sum += item.GetTotal());
            ViewBag.TotalPrice =sum ;


            return View(cart);
        }
    }
}
