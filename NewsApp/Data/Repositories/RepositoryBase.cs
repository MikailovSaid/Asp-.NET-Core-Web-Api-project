using Microsoft.EntityFrameworkCore;
using NewsApp.Core.Interfaces.Repository;
using NewsApp.Data;

namespace NewsApp.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        protected AppDbContext DbContext { get; }

        protected DbSet<TEntity> Set => DbContext.Set<TEntity>();

        public RepositoryBase(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Insert(TEntity entity)
        {
            Set.Add(entity);
        }

        public void InsertMany(IEnumerable<TEntity> entities)
        {
            Set.AddRange(entities);
        }

        public Task InsertAsync(TEntity entity)
        {
            return Set.AddAsync(entity).AsTask();
        }

        public Task InsertManyAsync(IEnumerable<TEntity> entities)
        {
            return Set.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            Set.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            Set.Remove(entity);
        }

        public void DeleteMany(IEnumerable<TEntity> entities)
        {
            Set.RemoveRange(entities);
        }


        public async Task<TEntity[]> GetAllAsync()
        {
            return await Set.ToArrayAsync();
        }

        public async ValueTask<TEntity?> GetAsync(int id)
        {
            return await Set.FindAsync(id);
        }
    }
}
