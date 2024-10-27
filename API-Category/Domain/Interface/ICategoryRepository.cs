using Domain.Model;
namespace Domain.Interface;

public interface ICategoryRepository
{
    public Task<Category> SaveAsync(Category category);
    public Task<IEnumerable<Category>> GetCategoriesAsync();
    public Task<Category> GetCategoryByIdAsync(Guid id);
}
