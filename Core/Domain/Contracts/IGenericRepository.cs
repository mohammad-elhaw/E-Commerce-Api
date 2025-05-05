using Domain.Entities;

namespace Domain.Contracts;
public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<List<TEntity>> GetAllAsync(bool withTrack);
    Task<TEntity?> GetByIdAsync(Specifications<TEntity> specifications);
    Task<int> Count(Specifications<TEntity> specifications);
    Task<List<TEntity>> GetAllAsync(Specifications<TEntity> specifications);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
