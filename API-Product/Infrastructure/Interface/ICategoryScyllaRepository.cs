using Domain.Model;

namespace Infrastructure.Interface;

public interface ICategoryScyllaRepository
{
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<Category>?> GetCategoriesAsync();
}
