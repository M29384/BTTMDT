using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _env;

        public OrderController(MyDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.OrderByDescending(p => p.Id).ToListAsync());
        }
        public async Task<IActionResult> ViewOrder(string ordercode)
        {
            var OrderDetail = await _context.orderDetails.Include(od => od.Product).Where(od => od.OrderCode==ordercode).ToListAsync();
            return View(await _context.Orders.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
