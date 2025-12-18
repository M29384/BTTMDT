using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class CategoryModel
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}
