﻿using Persistance.Data;

namespace Persistance.Repositories;
public class GenericRepository<TEntity, TKey>(AppDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);
    public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);
    public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

    public async Task<List<TEntity>> GetAllAsync(bool withTrack) =>
        withTrack ?
        await _dbContext.Set<TEntity>().ToListAsync() :
        await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

    public async Task<TEntity?> GetByIdAsync(TKey id) =>
        await _dbContext.Set<TEntity>().FindAsync(id);

    public async Task<TEntity?> GetByIdAsync(Specifications<TEntity> specifications) =>
        await ApplySpecification(specifications).FirstOrDefaultAsync();

    public async Task<List<TEntity>> GetAllAsync(Specifications<TEntity> specifications) =>
        await ApplySpecification(specifications).ToListAsync();

    public async Task<int> Count(Specifications<TEntity> specifications) =>
        await ApplySpecification(specifications).CountAsync();

    private IQueryable<TEntity> ApplySpecification(Specifications<TEntity> specifications) =>
        SpecificationEvaluator.GetQuery(_dbContext.Set<TEntity>(), specifications);

}
