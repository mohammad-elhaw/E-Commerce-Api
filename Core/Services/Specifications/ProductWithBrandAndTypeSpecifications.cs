using Domain.Contracts;
using Domain.Entities.ProductModule;
using Shared;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        // Retrieve product by id [include brand & type]
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        // Retrieve all Products include brand and types
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationsParameters parameters) 
            : base(product => 
                (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
                (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value) &&
                (string.IsNullOrWhiteSpace(parameters.Search) || 
                product.Name.ToLower().Contains(parameters.Search.ToLower().Trim()))
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {
                    case ProductSortOptions.PriceDesc:
                        SetOrderByDescending(p => p.Price);
                        break;
                    case ProductSortOptions.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case ProductSortOptions.NameDesc:
                        SetOrderByDescending(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }
            ApplyPaging(parameters.PageIndex, parameters.PageSize);
        }
    }
}
