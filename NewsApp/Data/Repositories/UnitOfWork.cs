using NewsApp.Core.Interfaces;
using NewsApp.Data;

namespace NewsApp.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private readonly ILogger<UnitOfWork> logger;

        private int transactionDepth;

        public UnitOfWork(AppDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            if (++transactionDepth > 1)
            {
                logger.LogDebug($"Emulating starting nested transaction, transaction depth: {transactionDepth}");

                return;
            }

            logger.LogDebug("Starting transaction");

            await dbContext.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            if (--transactionDepth > 0)
            {
                logger.LogDebug($"Emulating nested transaction commit, transaction depth: {transactionDepth}");

                return;
            }

            logger.LogDebug("Committing transaction");

            dbContext.Database.CommitTransaction();
        }

        public void Rollback()
        {
            if (--transactionDepth > 0)
            {
                logger.LogDebug($"Emulating nested transaction rollback, transaction depth: {transactionDepth}");

                return;
            }

            logger.LogDebug("Rolling back transaction");

            dbContext.Database.RollbackTransaction();
        }
    }
}
