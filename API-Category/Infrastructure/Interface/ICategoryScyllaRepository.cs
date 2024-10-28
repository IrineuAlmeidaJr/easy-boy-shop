using Infrastructure.Repository.Entity;

namespace Infrastructure.Interface;

public interface ICategoryScyllaRepository
{
    Task<CategoryEntity> SaveAsync(CategoryEntity entity);
    Task<CategoryEntity?> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<CategoryEntity>?> GetCategoriesAsync();
}
