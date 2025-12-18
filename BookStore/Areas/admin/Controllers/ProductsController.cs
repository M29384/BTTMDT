using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Data; // added for IsolationLevel

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductsController(MyDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.OrderByDescending(p => p.ProductId).Include(p => p.Category).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,TenTg,NhaXuatBan,Price,Soluong,ImageUrl,Description,CategoryId,Nxb,ImageUpload")] ProductModel product)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Một vài thứ đang bị lỗi!";
                ViewData["CategoryId"] = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }

            // handle upload
            if (product.ImageUpload != null)
            {
                var fileName = await SaveImageAsync(product.ImageUpload);
                if (fileName != null)
                {
                    product.ImageUrl = fileName;
                }
            }

            // If your database does not generate ids automatically (no IDENTITY),
            // assign the next numeric id in a serializable transaction to reduce races.
            // If your DB already uses IDENTITY/auto-increment, remove this block and let the DB generate the id.
            await using (var tx = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable))
            {
                try
                {
                    var maxId = await _context.Products.MaxAsync(p => (int?)p.ProductId) ?? 0;
                    product.ProductId = maxId + 1;

                    _context.Add(product);
                    await _context.SaveChangesAsync();

                    await tx.CommitAsync();
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            }

            TempData["Success"] = "Tạo sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,TenTg,NhaXuatBan,Price,Soluong,ImageUrl,Description,CategoryId,Nxb,ImageUpload")] ProductModel product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null) return NotFound();

            // update scalar properties
            dbProduct.ProductName = product.ProductName;
            dbProduct.TenTg = product.TenTg;
            dbProduct.NhaXuatBan = product.NhaXuatBan;
            dbProduct.Nxb = product.Nxb;
            dbProduct.Price = product.Price;
            dbProduct.Soluong = product.Soluong;
            dbProduct.Description = product.Description;
            dbProduct.CategoryId = product.CategoryId;

            // handle image upload: replace and delete old file
            if (product.ImageUpload != null)
            {
                var fileName = await SaveImageAsync(product.ImageUpload);
                if (fileName != null)
                {
                    // delete old file if it exists and is not empty
                    if (!string.IsNullOrEmpty(dbProduct.ImageUrl))
                    {
                        var oldPath = Path.Combine(_env.WebRootPath ?? string.Empty, "assets", "images", dbProduct.ImageUrl);
                        if (System.IO.File.Exists(oldPath))
                        {
                            try { System.IO.File.Delete(oldPath); } catch { /* ignore */ }
                        }
                    }
                    dbProduct.ImageUrl = fileName;
                }
            }
            else
            {
                // if user cleared ImageUrl in the form, keep or set as needed
                dbProduct.ImageUrl = product.ImageUrl;
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cập nhật sản phẩm thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(dbProduct.ProductId))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'MyDbContext.Products'  is null.");
            }

            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var path = Path.Combine(_env.WebRootPath ?? string.Empty, "assets", "images", product.ImageUrl);
                    if (System.IO.File.Exists(path))
                    {
                        try { System.IO.File.Delete(path); } catch { /* ignore */ }
                    }
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa sản phẩm thành công!";
            }
            else
            {
                TempData["Error"] = "Không tìm thấy sản phẩm để xóa.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

        private async Task<string?> SaveImageAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0) return null;

            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext)) return null;

            var uploads = Path.Combine(_env.WebRootPath ?? string.Empty, "assets", "images");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}