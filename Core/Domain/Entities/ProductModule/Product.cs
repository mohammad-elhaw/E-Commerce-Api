namespace Domain.Entities.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        
        public ProductBrand ProductBrand { get; set; } // Navigational Property [One]
        public int BrandId { get; set; } // FK
        public ProductType ProductType { get; set; } // // Navigational Property [One]
        public int TypeId { get; set; } // FK
    }
}
