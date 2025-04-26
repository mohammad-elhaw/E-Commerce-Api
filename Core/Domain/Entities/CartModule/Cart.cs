namespace Domain.Entities.CartModule
{
    public class Cart
    {
        public string Id { get; set; }
        public ICollection<CartItem> Items { get; set; } = [];
    }
}
