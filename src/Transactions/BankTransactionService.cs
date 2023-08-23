using Microsoft.AspNetCore.Authorization;

namespace LClaproth.MyFinancialTracker.Transactions;

[Authorize(AuthenticationSchemes = "Bearer")]
public class BankTransactionService : Bank.Handler.HandlerBase
{
    private readonly ILogger<BankTransactionService> _logger;
    private readonly ITransactionRepository<Bank.Transaction> _repository;
    public BankTransactionService(ITransactionRepository<Bank.Transaction> repository, ILogger<BankTransactionService> logger)
    {
        _logger = logger;
        _repository = repository;
    }

    public override async Task<Bank.Response> Transactions(Bank.Empty request, ServerCallContext context)
    {
        var user = context.GetHttpContext().User;
        var data = new Bank.Data{
            Source = "MongoDB"
        };

        data.TransactionList.AddRange(_repository.GetAll());
        return new Bank.Response{
            Data = data,
            Message = "Ok."
        };
    }

    public override async Task<Bank.Response> AddTransactions(Bank.TransactionParameters transactions, ServerCallContext context){
        _repository.InsertMany(transactions.Transactions.AsEnumerable());
        return new Bank.Response {
            Message = "Successfully created bank transactions"
        };
    }
}
