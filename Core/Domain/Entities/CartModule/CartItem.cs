namespace Domain.Entities.CartModule
{
    public class CartItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
