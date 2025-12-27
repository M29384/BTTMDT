using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models;

public partial class ProductModel
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? TenTg { get; set; }

    public string? NhaXuatBan { get; set; }

    public decimal? Price { get; set; }

    public int? Soluong { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public DateOnly? Nxb { get; set; }

    public virtual ICollection<CartModel> Carts { get; set; } = new List<CartModel>();

    public virtual CategoryModel? Category { get; set; }

    [NotMapped]
    public IFormFile? ImageUpload { get; set; }
    }

