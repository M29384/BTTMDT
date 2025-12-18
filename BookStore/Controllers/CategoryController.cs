using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using NuGet.Packaging.Signing;
namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MyDbContext _context;

        public CategoryController(MyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            var products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult Detail(int id)
        {
            if (id == null) return RedirectToAction("Index");

            var productbyID =  _context.Products
                                     .Where(p => p.ProductId == id)
                                     .FirstOrDefault();
            return View(productbyID); 
        }


    }
}
