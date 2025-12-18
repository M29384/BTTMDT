namespace BookStore.Models.ViewModels
{
    public class CartView
    {
        public List<CartModel> cartItems { get; set; }
        public decimal totalprice { get; set; }
    }
}
