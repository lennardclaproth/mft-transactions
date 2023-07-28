using Microsoft.EntityFrameworkCore;

namespace MyFinancialTracker.Transactions.EntityFrameworkCore;

public class TransactionContext : DbContext
{
    public DbSet<BankTransaction>? BankTransactions { get; set; }

    public TransactionContext(DbContextOptions<TransactionContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankTransaction>().HasKey(tx => tx.Id);
        modelBuilder.Entity<BankTransaction>()
            .Property(tx => tx.Id)
            .ValueGeneratedOnAdd();
    }
}