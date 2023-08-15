using Microsoft.EntityFrameworkCore;

namespace MyFinancialTracker.Transactions.EntityFrameworkCore;

public class TransactionContext : DbContext
{
    public DbSet<Bank.Transaction>? BankTransactions { get; set; }

    public TransactionContext(DbContextOptions<TransactionContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bank.Transaction>().HasKey(tx => tx.Id);
        modelBuilder.Entity<Bank.Transaction>()
            .Property(tx => tx.Id)
            .ValueGeneratedOnAdd();
    }
}