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

    public Task<Stock> SaveAsync(Stock entity)
    {
        if (entity.Id == null)
            return Insert(entity);
        return Update(entity);
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

    public async Task<Stock?> GetByIdAsync(Guid id)
    {
        if (id == null) 
            return null;

        var query = @"SELECT * FROM stock WHERE id = ?";
        var statement = new SimpleStatement(query, id);
        var row = await _context.GetSession().ExecuteAsync(statement);
        var logRow = row.FirstOrDefault();

        if (logRow == null)
            return null;

        return ToModel(logRow);
    }

    private async Task<Stock> Insert(Stock entity)
    {
        entity.Id = Guid.NewGuid();

        var query = @"INSERT INTO stock (
                        id, 
                        product_id,
                        quantity
                    ) VALUES (?,?,?)";

        var statement = new SimpleStatement(
            query,
            entity.Id,
            entity.ProductId,
            entity.Quantity
         );

        await _context.GetSession().ExecuteAsync(statement);

        return entity;
    }

    private async Task<Stock> Update(Stock entity)
    {
        var query = "UPDATE stock SET quantity=? WHERE id=?";

        var statement = new SimpleStatement(
            query,
            entity.Quantity,
            entity.Id
         );

        await _context.GetSession().ExecuteAsync(statement);
        return entity;
    }

    private Stock ToModel(Row row) => new Stock(row.GetValue<Guid>("id"), row.GetValue<Guid>("product_id"), row.GetValue<int>("quantity"));

}
