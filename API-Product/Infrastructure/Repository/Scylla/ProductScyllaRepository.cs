using Cassandra;
using Infrastructure.Interface;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Repository.Scylla;

public class ProductScyllaRepository : IProductScyllaRepository
{
    private readonly IScyllaContext _context;

    public ProductScyllaRepository(IScyllaContext context)
    {
        _context = context;
    }

    public Task<ProductEntity> SaveAsync(ProductEntity entity)
    {
        if (entity.Id == null)
            return Insert(entity);
        return Update(entity);
    }

    public async Task<ProductEntity?> GetByIdAsync(Guid id)
    {
        var query = @"SELECT * FROM product WHERE id = ?";

        var statement = new SimpleStatement(query, id);
        var row = await _context.GetSession().ExecuteAsync(statement);
        var logRow = row.FirstOrDefault();

        if (logRow == null)
            return null;

        return ToEntity(logRow);
    }

    public async Task<IEnumerable<ProductEntity>?> GetProducts()
    {
        var query = @"SELECT * FROM product";

        var statement = new SimpleStatement(query);
        var rows = await _context.GetSession().ExecuteAsync(statement);

        if (rows == null)
            return null;

        return rows.Select(row => ToEntity(row)).ToList();
    }


    private async Task<ProductEntity> Insert(ProductEntity entity)
    {
        entity.Id = Guid.NewGuid();

        var operationTimestamp = DateTime.UtcNow;
        entity.CreatedAt = operationTimestamp;
        entity.UpdatedAt = operationTimestamp;

        var query = @"INSERT INTO product (
                        id, 
                        name,
                        seller_id,
                        category_id,
                        price,
                        created_at, 
                        updated_at
                    ) VALUES (?,?,?,?,?,?,?)";

        var statement = new SimpleStatement(
            query,
            entity.Id,
            entity.Name,
            entity.SellerId,
            entity.CategoryId,
            entity.Price,
            entity.CreatedAt,
            entity.UpdatedAt
         );

        await _context.GetSession().ExecuteAsync(statement);

        return entity;
    }

    private async Task<ProductEntity> Update(ProductEntity entity)
    {
        var query = "UPDATE product SET name=?, price=? WHERE id=?";
        entity.UpdatedAt = DateTime.UtcNow;

        var statement = new SimpleStatement(
           query,
           entity.Name,
           entity.Price,
           entity.Id
        );

        await _context.GetSession().ExecuteAsync(statement);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        // Exclui Stock
        var query = @"DELETE FROM stock WHERE product_id = ?";
        var statement = new SimpleStatement(query, id);
        var row = await _context.GetSession().ExecuteAsync(statement);

        // Excluir Product
        query = @"DELETE FROM product WHERE id = ?";
        statement = new SimpleStatement(query, id);
        row = await _context.GetSession().ExecuteAsync(statement);

        return row.Any();
    }

    private ProductEntity ToEntity(Row row) => new ProductEntity
    {
        Id = row.GetValue<Guid>("id"),
        Name = row.GetValue<string>("name"),
        CategoryId = row.GetValue<Guid>("category_id"),        
        SellerId = row.GetValue<Guid>("seller_id"),
        Price = row.GetValue<decimal>("price"),
        CreatedAt = row.GetValue<DateTime>("created_at"),
        UpdatedAt = row.GetValue<DateTime>("updated_at")
    };
}
