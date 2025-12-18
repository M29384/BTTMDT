using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class CartModel
{
    public int CartId { get; set; }

    public int? ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? tenTg { get; set; }

    public decimal? Price { get; set; }

    public string? imageUrl { get; set; }

    public int? Soluong { get; set; }

    public int? UsersId { get; set; }

    public virtual ProductModel? Product { get; set; }

    public decimal? Tong { get { return Soluong * Price; } }

    public CartModel()
    {
        
    }
    public CartModel(ProductModel product)
    {
        ProductId = product.ProductId;
        ProductName = product.ProductName;
        tenTg = product.TenTg;
        Price = product.Price;
        imageUrl = product.ImageUrl;
        Soluong = 1;
    }
}
