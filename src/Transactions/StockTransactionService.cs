using Grpc.Core;
using LClaproth.MyFinancialTracker.Transactions.EntityFrameworkCore;

namespace LClaproth.MyFinancialTracker.Transactions;

public class StockTransactionService : Stock.Handler.HandlerBase
{
    private readonly ILogger<BankTransactionService> _logger;
    private readonly ITransactionRepository<Stock.Transaction> _repository;
    public StockTransactionService(ITransactionRepository<Stock.Transaction> repository, ILogger<BankTransactionService> logger)
    {
        _logger = logger;
        _repository = repository;
    }

    public override async Task<Stock.Response> Transactions(Stock.Empty request, ServerCallContext context)
    {
        var user = context.GetHttpContext().User;
        var data = new Stock.Data{
            Source = "MongoDB"
        };

        data.TransactionList.AddRange(_repository.GetAll());
        return new Stock.Response{
            Data = data,
            Message = "Ok."
        };
    }

    public override async Task<Stock.Response> AddTransactions(Stock.TransactionParameters transactions, ServerCallContext context){
        _repository.InsertMany(transactions.Transactions.AsEnumerable());
        return new Stock.Response {
            Message = "Successfully created bank transactions"
        };
    }
}
