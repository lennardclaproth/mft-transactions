using Microsoft.EntityFrameworkCore;

namespace MyFinancialTracker.Transactions;

public class StockTransactionContext : DbContext
{
    public DbSet<StockTransaction>? StockTransactions { get; set; }

    public StockTransactionContext(DbContextOptions<StockTransactionContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StockTransaction>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
}