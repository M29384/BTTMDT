using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly MyDbContext _context;

        public CartController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CartModel> CartItems = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();
            CartView cartVM = new()
            {
                cartItems = CartItems,
                totalprice = CartItems.Sum(item => (item.Soluong ?? 0) * (item.Price ?? 0))
            };
            return View(cartVM);
        }
        public async Task<IActionResult> Add(int Id)
        {
            ProductModel? product = await _context.Products.FindAsync(Id);
            List<CartModel> cart = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();
            CartModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();
            if (cartItems == null)
            {
                cart.Add(new CartModel(product));
            }
            else
            {
                cartItems.Soluong += 1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        
        public IActionResult Increase(int id)
        {
            List<CartModel> cart = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();
            CartModel? item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                item.Soluong = (item.Soluong ?? 0) + 1;
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        
        public IActionResult Decrease(int id)
        {
            List<CartModel> cart = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();
            CartModel? item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                int newQty = (item.Soluong ?? 0) - 1;
                if (newQty <= 0)
                {
                    cart.Remove(item);
                }
                else
                {
                    item.Soluong = newQty;
                }
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        
        public IActionResult Remove(int id)
        {
            List<CartModel> cart = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();
            CartModel? item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
    }
}
