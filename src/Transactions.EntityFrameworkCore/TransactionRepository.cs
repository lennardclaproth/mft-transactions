using Microsoft.EntityFrameworkCore;

namespace MyFinancialTracker.Transactions.EntityFrameworkCore
{
    public interface ITransactionRepository<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> GetAll();
        public void InsertMany(IEnumerable<TEntity> entities);
        public void SaveChanges();
    }

    public class TransactionRepository<TEntity> : ITransactionRepository<TEntity> where TEntity : class
    {
        private readonly TransactionContext _context;
        public TransactionRepository(TransactionContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public void InsertMany(IEnumerable<TEntity> entities)
        {
            _context.AddRange(entities);
            int addedCount = _context.ChangeTracker.Entries<TEntity>()
            .Count(e => e.State == EntityState.Added);
        }

        public void SaveChanges(){
            _context.SaveChanges();
        }
    }
}
