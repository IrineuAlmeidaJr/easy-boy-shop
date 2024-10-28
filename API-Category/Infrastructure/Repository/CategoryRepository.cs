using Domain.Model;
using Domain.Interface;
using Infrastructure.Interface;

namespace Infrastructure.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ICategoryScyllaRepository _categoryRepository;
    private readonly ICategoryAdapter _categoryAdapter;

    public CategoryRepository(ICategoryScyllaRepository categoryRepository, ICategoryAdapter categoryAdapter)
    {
        _categoryRepository = categoryRepository;
        _categoryAdapter = categoryAdapter;
    }

    public async Task<Category> SaveAsync(Category category)
    {
        var entity = _categoryAdapter.ToCategoryEntity(category);
        var storedEntity = await _categoryRepository.SaveAsync(entity);
        var storedCategory = _categoryAdapter.FromCategoryEntity(storedEntity);

        return storedCategory;
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        var storedEntity = await _categoryRepository.GetCategoryByIdAsync(id);

        if (storedEntity == null)
            return null;

        return _categoryAdapter.FromCategoryEntity(storedEntity);
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        var storedEntity = await _categoryRepository.GetCategoriesAsync();

        if (storedEntity == null)
            return Enumerable.Empty<Category>();
        
        var storedCategories = storedEntity.Select(stored => _categoryAdapter.FromCategoryEntity(stored)).ToList();

        return storedCategories;
    }
}
