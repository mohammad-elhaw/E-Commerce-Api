
using Persistance.Data;
using System.Collections.Concurrent;

namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        //private Dictionary<string, object> _repositories;
        private ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new ();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey> =>
            (GenericRepository<TEntity, TKey>) _repositories.
                GetOrAdd(typeof(TEntity).Name, (_) =>new GenericRepository<TEntity, TKey>(_dbContext));
        
            //var typeName = typeof(TEntity).Name;
            //if(!_repositories.ContainsKey(typeName))
            //{
            //    _repositories.TryAdd(typeName,new GenericRepository<TEntity, TKey>(_dbContext));
            //}

            //return (IGenericRepository<TEntity, TKey>)_repositories[typeName];

        

        public async Task<int> SaveChangesAsync() =>
            await _dbContext.SaveChangesAsync();
    }
}
