using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class OrderController : Controller
    {

        private ApplicationDbContext _dbContext;

        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public IActionResult Index()
        {
            List<Order> orders = _dbContext.Order.ToList();
            foreach (var order in orders)
            {
                order.OrderItems = (
                    from orderItem in _dbContext.OrderItem
                    where orderItem.Id == order.Id
                    select new OrderItem
                    {
                        Total = orderItem.Total

                    }).ToList();

                foreach (var orderItem in order.OrderItems)
                {
                    order.Total += orderItem.Total;
                }
            }

            return View(orders);
        }





        public IActionResult Details(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }

            var order = _dbContext.Order.FirstOrDefault(o => o.Id == id);


            if (order == null)
            {
                return NotFound();
            }


            order.OrderItems = (

                from orderItem in _dbContext.OrderItem
                where orderItem.Id == order.Id
                select new OrderItem
                {
                    Id = orderItem.Id,
                    OrderId = orderItem.OrderId,
                    ProductId = orderItem.ProductId,
                    ProductTitle = _dbContext.Products.SingleOrDefault(p=> p.Id==orderItem.ProductId).Title,
                    Quantity = orderItem.Quantity,
                    Total = orderItem.Total,

                }



                ).ToList();

            foreach (var orderItem in order.OrderItems)
            {
                order.Total += orderItem.Total;
            }

            return View(order);
        }


        public IActionResult Create()
        {

            ViewBag.Users = _dbContext.Users.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.FirstName + " " + u.LastName,
            }).ToList();
            
            return View();
      
       
        
        }


        [HttpPost]

        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Order.Add(order);
                _dbContext.SaveChanges();

                TempData["Success"] = "Narudžba uspješna!";


                return RedirectToAction("Index");
            }




            ViewBag.Users = _dbContext.Users.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.FirstName + " " + u.LastName,
            }).ToList();




            return View(order);
        
        }
    
    
    
    }
}
