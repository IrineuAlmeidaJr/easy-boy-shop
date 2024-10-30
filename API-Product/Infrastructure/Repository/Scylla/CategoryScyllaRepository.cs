using Cassandra;
using Cassandra.Data.Linq;
using Domain.Model;
using Infrastructure.Interface;
using Infrastructure.Interfaces;

namespace Infrastructure.Repository.Scylla;

public class CategoryScyllaRepository : ICategoryScyllaRepository
{
    private readonly IScyllaContext _context;

    public CategoryScyllaRepository(IScyllaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>?> GetCategoriesAsync()
    {
        var query = @"SELECT * FROM category";
        var statement = new SimpleStatement(query);
        var rows = await _context.GetSession().ExecuteAsync(statement);

        if (rows == null)
            return null;

        return rows.Select(row => ToModel(row)).ToList();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        var query = @"SELECT * FROM category WHERE id = ?";
        var statement = new SimpleStatement(query, id);
        var row = await _context.GetSession().ExecuteAsync(statement);
        var logRow = row.FirstOrDefault();

        if (logRow == null)
            return null;

        return ToModel(logRow);
    }

    private Category ToModel(Row row) => new Category(row.GetValue<Guid>("id"), row.GetValue<string>("name"));
}
