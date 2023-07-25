namespace MyFinancialTracker.Transactions;

public class Transaction {
    public int Id { get; set; }
    public DateTime Date { get ; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Hash { get; set; }
}

public class BankTransaction : Transaction {
    public string Source { get; set; }
    public string Statement { get; set; }
}

public class StockTransaction : Transaction {
    public string ISIN { get; set; }
    public double PricePerShar { get; set; }
    public string Currency { get; set; }
    public string? ExchangeRate { get; set; }
    public double TransactionFee { get; set; }
    public int Amount { get; set; }
}