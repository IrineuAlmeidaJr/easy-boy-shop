using Cassandra;
using Cassandra.Data.Linq;
using Infrastructure.Interface;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Repository.Scylla;

public class CategoryScyllaRepository : ICategoryScyllaRepository
{
    private readonly IScyllaContext _context;

    public CategoryScyllaRepository(IScyllaContext context)
    {
        _context = context;
    }

    public Task<CategoryEntity> SaveAsync(CategoryEntity entity)
    {
        if (entity.Id == null)
            return Insert(entity);
        return Update(entity);
    }
    public async Task<IEnumerable<CategoryEntity>?> GetCategoriesAsync()
    {
        var query = @"SELECT * FROM category";
        var statement = new SimpleStatement(query);
        var rows = await _context.GetSession().ExecuteAsync(statement);

        if (rows == null)
            return null;

        return rows.Select(row => ToEntity(row)).ToList();
    }

    public async Task<CategoryEntity?> GetCategoryByIdAsync(Guid id)
    {
        var query = @"SELECT * FROM category WHERE id = ?";
        var statement = new SimpleStatement(query, id);
        var row = await _context.GetSession().ExecuteAsync(statement);
        var logRow = row.FirstOrDefault();

        if (logRow == null)
            return null;

        return ToEntity(logRow);
    }

    public async Task<CategoryEntity> Insert(CategoryEntity entity)
    {
        entity.Id = Guid.NewGuid();
        var operationTimestamp = DateTime.UtcNow;

        var query = @"INSERT INTO category (
                            id,
                            nome) VALUES (?,?)";
        var statement = new SimpleStatement(
            query,
            entity.Id,
            entity.Name
        );

        await _context.GetSession().ExecuteAsync(statement);

        return entity;
    }

    private async Task<CategoryEntity> Update(CategoryEntity entity)
    {
        var query = "UPDATE category SET name=? WHERE id=?";

        var statement = new SimpleStatement(query, entity.Name, entity.Id);
        await _context.GetSession().ExecuteAsync(statement);
        return entity;
    }

    private CategoryEntity ToEntity(Row row) => new CategoryEntity
    {
        Id = row.GetValue<Guid>("id"),
        Name = row.GetValue<string>("name")
    };
}
