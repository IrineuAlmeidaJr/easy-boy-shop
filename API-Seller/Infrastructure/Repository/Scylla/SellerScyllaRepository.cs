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

    public Task<SellerEntity> SaveAsync(SellerEntity entity)
    {
        if (entity.Id == null)
            return Insert(entity);
        return Update(entity);
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


    private async Task<SellerEntity> Insert(SellerEntity entity)
    {
        entity.Id = Guid.NewGuid();

        var operationTimestamp = DateTime.UtcNow;
        entity.CreatedAt = operationTimestamp;
        entity.UpdatedAt = operationTimestamp;

        var query = @"INSERT INTO seller (
                        id, 
                        created_at, 
                        updated_at, 
                        name,
                        cnpj,
                        telefone, 
                        email,
                        logradouro,
                        cidade,
                        estado    
                    ) VALUES (?,?,?,?,?,?,?,?,?,?)";

        var statement = new SimpleStatement(
            query,
            entity.Id,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.Name,
            entity.CNPJ,
            entity.Telefone,
            entity.Email,   
            entity.Logradouro,
            entity.Cidade,
            entity.Estado
         );

        await _context.GetSession().ExecuteAsync(statement);

        return entity;
    }

    private async Task<SellerEntity> Update(SellerEntity entity)
    {
        var query = @"UPDATE seller SET 
                    name = ?, 
                    cnpj = ?, 
                    telefone = ?, 
                    email = ?, 
                    logradouro = ?, 
                    cidade = ?, 
                    estado = ?, 
                    updated_at = ? 
                  WHERE id = ?";
        entity.UpdatedAt = DateTime.UtcNow;

        var statement = new SimpleStatement(
            query,
            entity.Name,
            entity.CNPJ,
            entity.Telefone,
            entity.Email,
            entity.Logradouro,
            entity.Cidade,
            entity.Estado,
            entity.UpdatedAt,
            entity.Id
        );

        await _context.GetSession().ExecuteAsync(statement);
        return entity;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var query = @"DELETE FROM seller WHERE id = ?";
        var statement = new SimpleStatement(query, id);

        var row = await _context.GetSession().ExecuteAsync(statement);

        return row.Any();
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
