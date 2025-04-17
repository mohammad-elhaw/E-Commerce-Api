using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        // Signature for function will return an instance of class that implement IGenericRepo
        // GenericRepository<ProductBrand, int>
        // GenericRepository<ProductType, int>
        // GenericRepository<Product, int>
    
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}
