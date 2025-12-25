using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class OrderDetail
    {
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? OrderCode { get; set; }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public int? Soluong {  get; set; }
        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }
    }
}
