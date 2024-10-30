using Cassandra;
using Infrastructure.Interface;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Repository.Scylla;

public class SellerScyllaRepository : ISellerScyllaRepository
{
    private readonly IScyllaContext _context;

    public SellerScyllaRepository(IScyllaContext context)
    {
        _context = context;
    }

    public async Task<SellerEntity?> GetByIdAsync(Guid id)
    {
        var query = @"SELECT * FROM seller WHERE id = ?";

        var statement = new SimpleStatement(query, id);
        var row = await _context.GetSession().ExecuteAsync(statement);
        var logRow = row.FirstOrDefault();

        if (logRow == null)
            return null;

        return ToEntity(logRow);
    }

    public async  Task<IEnumerable<SellerEntity>?> GetSellers()
    {
        var query = @"SELECT * FROM seller";

        var statement = new SimpleStatement(query);
        var rows = await _context.GetSession().ExecuteAsync(statement);

        if (rows == null)
            return null;

        return rows.Select(row => ToEntity(row)).ToList();
    }
    private SellerEntity ToEntity(Row row) => new SellerEntity
    {
        Id = row.GetValue<Guid>("id"),
        CreatedAt = row.GetValue<DateTime>("created_at"),
        UpdatedAt = row.GetValue<DateTime>("updated_at"),
        Name = row.GetValue<string>("name"),
        CNPJ = row.GetValue<string>("cnpj"),
        Telefone = row.GetValue<string>("telefone"),
        Email = row.GetValue<string>("email"),
        Logradouro = row.GetValue<string>("logradouro"),
        Cidade = row.GetValue<string>("cidade"),
        Estado = row.GetValue<string>("estado"),
    };
}
