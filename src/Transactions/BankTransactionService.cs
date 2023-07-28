using Grpc.Core;
using MyFinancialTracker.Transactions.EntityFrameworkCore;

namespace MyFinancialTracker.Transactions;

public class BankTransactionService : BankTransactionHandler.BankTransactionHandlerBase
{
    private readonly ILogger<BankTransactionService> _logger;
    private readonly ITransactionRepository<BankTransaction> _repository;
    public BankTransactionService(ITransactionRepository<BankTransaction> repository, ILogger<BankTransactionService> logger)
    {
        _logger = logger;
        _repository = repository;
    }

    public override Task<BankTransactionsReply> BankTransactions(Empty request, ServerCallContext context)
    {
        var result = new BankTransactionsReply();
        result.BankTransactions.AddRange(_repository.GetAll());
        return Task.FromResult(result);
    }
}
