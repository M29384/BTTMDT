using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
