namespace MyFinancialTracker.Transactions;

public class BankTransactionService
{
    private readonly BankTransactionRepository _repository;

    public BankTransactionService(BankTransactionRepository repository)
    {
        _repository = repository;
    }

    public List<BankTransaction> BankTransactions()
    {
        return _repository.AllTransactions().ToList();
    }
}