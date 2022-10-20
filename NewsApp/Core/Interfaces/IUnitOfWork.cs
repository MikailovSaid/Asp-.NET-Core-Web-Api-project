namespace NewsApp.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();

        Task BeginTransactionAsync();

        void Commit();

        void Rollback();
    }
}
