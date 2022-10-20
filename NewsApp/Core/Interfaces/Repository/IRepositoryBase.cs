namespace NewsApp.Core.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class
    {
        void Insert(TEntity entity);

        void InsertMany(IEnumerable<TEntity> entities);

        Task InsertAsync(TEntity entity);

        Task InsertManyAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteMany(IEnumerable<TEntity> entities);

        Task<TEntity[]> GetAllAsync();

        ValueTask<TEntity> GetAsync(int id);
    }
}
