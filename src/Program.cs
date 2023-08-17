using Microsoft.EntityFrameworkCore;
using MyFinancialTracker.Transactions;
using MyFinancialTracker.Transactions.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetValue<string>("TransactionsDb:ConnectionString");
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddDbContext<TransactionContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion)
                // The following three options help with debugging, but should
                // be changed or removed for production.
                // .LogTo(Console.WriteLine, LogLevel.Information)
                // .EnableSensitiveDataLogging()
                // .EnableDetailedErrors()
        );
builder.Services.AddScoped(typeof(ITransactionRepository<>), typeof(TransactionRepository<>));

var app = builder.Build();

app.MapGrpcService<BankTransactionService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.MapGrpcReflectionService();

app.Run();
