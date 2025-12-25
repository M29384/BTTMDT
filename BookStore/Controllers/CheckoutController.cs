using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly MyDbContext _myDbContext;
        public CheckoutController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        
        public async Task<IActionResult> Checkout()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var ordercode = Guid.NewGuid().ToString();
                var orderItem = new OrderModel();
                orderItem.OrderCode = ordercode;
                orderItem.UserName = userEmail;
                orderItem.CreatedDate = DateTime.Now;
                _myDbContext.Add(orderItem);
                _myDbContext.SaveChanges();
                List<CartModel> CartItems = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();
                foreach (var cart in CartItems) 
                {
                    var orderdetail = new OrderDetail();
                    orderdetail.UserName = userEmail;
                    orderdetail.OrderCode = ordercode;
                    orderdetail.ProductId = cart.ProductId;
                    orderdetail.Price = cart.Price;
                    orderdetail.Soluong = cart.Soluong;
                    _myDbContext.Add(orderdetail);
                    _myDbContext.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");
                TempData["success"] = "Checkout thành công";
                return RedirectToAction("Index", "Cart");
            }
            return View();
        }
    }
}
