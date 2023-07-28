using Microsoft.EntityFrameworkCore;

namespace MyFinancialTracker.Transactions.EntityFrameworkCore
{
    public interface ITransactionRepository<TEntity> where TEntity : class
    {
        public List<TEntity> GetAll();
        public void InsertMany(List<TEntity> entities);
        public void SaveChanges();
    }

    public class TransactionRepository<TEntity> : ITransactionRepository<TEntity> where TEntity : class
    {
        private readonly TransactionContext _context;
        public TransactionRepository(TransactionContext context)
        {
            _context = context;
        }

        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void InsertMany(List<TEntity> entities)
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
