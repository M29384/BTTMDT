namespace BookStore.Models
{
    public class OrderModel
    {
        public int? Id { get; set; }
        public string? OrderCode { get; set; }
        public string? UserName { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
