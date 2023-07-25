using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using MyFinancialTracker.Transactions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("transactions-db").GetSection("ConnectionString").Value;
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<BankTransactionContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion)
                // The following three options help with debugging, but should
                // be changed or removed for production.
                // .LogTo(Console.WriteLine, LogLevel.Information)
                // .EnableSensitiveDataLogging()
                // .EnableDetailedErrors()
        );

builder.Services.AddDbContext<StockTransactionContext>(
    dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion)
);

builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddScoped<BankTransactionService>();
builder.Services.AddScoped<BankTransactionRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
