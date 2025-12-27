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
        public IActionResult search(string searchString)
        {
            var product = from m in _context.Products
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.ProductName.Contains(searchString));
            }
            return View(product.ToList());
        }

        public async Task<IActionResult> Sort(string sortOrder, int? categoryId)
        {
            var products = _context.Products.AsQueryable();

            if (categoryId.HasValue && categoryId > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            switch (sortOrder)
            {
                case "asc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "new":
                    products = products.OrderByDescending(p => p.Nxb);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductName);
                    break;
            }
            var result = await products.ToListAsync();

            return View(result);
        }



    }
}
