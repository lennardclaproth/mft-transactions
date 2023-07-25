using Microsoft.EntityFrameworkCore;

namespace MyFinancialTracker.Transactions;
public class BankTransactionRepository
{
    private readonly BankTransactionContext _context;
    public BankTransactionRepository(BankTransactionContext context)
    {
        _context = context;
    }

    public void InsertMany(List<BankTransaction> transactions)
    {
        _context.AddRange(transactions);
        int addedCount = _context.ChangeTracker.Entries<BankTransaction>()
        .Count(e => e.State == EntityState.Added);
    }

    public IEnumerable<BankTransaction> AllTransactions()
    {
        return _context.BankTransactions.ToList();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}