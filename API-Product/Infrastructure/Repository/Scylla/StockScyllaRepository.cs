using Cassandra;
using Domain.Model;
using Infrastructure.Interface;
using Infrastructure.Interfaces;

namespace Infrastructure.Repository.Scylla;

public class StockScyllaRepository : IStockScyllaRepository
{
    private readonly IScyllaContext _context;

    public StockScyllaRepository(IScyllaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Stock>?> GetStocksAsync()
    {
        var query = @"SELECT * FROM stock";
        var statement = new SimpleStatement(query);
        var rows = await _context.GetSession().ExecuteAsync(statement);

        if (rows == null)
            return null;

        return rows.Select(row => ToModel(row)).ToList();
    }

    public async Task<Stock?> GetStockByProductIdAsync(Guid? productId)
    {
        if (productId == null) 
            return null;

        var query = @"SELECT * FROM stock WHERE product_id = ? ALLOW FILTERING";
        var statement = new SimpleStatement(query, productId);
        var row = await _context.GetSession().ExecuteAsync(statement);
        var logRow = row.FirstOrDefault();

        if (logRow == null)
            return null;

        return ToModel(logRow);
    }

    private Stock ToModel(Row row) => new Stock(row.GetValue<Guid>("id"), row.GetValue<Guid>("product_id"), row.GetValue<int>("quantity"));
}
