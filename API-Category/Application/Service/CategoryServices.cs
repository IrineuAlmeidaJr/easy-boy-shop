using Application.DTO;
using Application.Interface;
using Domain.Interface;
using Domain.Exception;

namespace Application.Service;

public class CategoryServices : ICategoryServices
{
    private ICategoryRepository _categoryRepository;
    private ICategoryMapper _categoryMapper;

    public CategoryServices(ICategoryRepository categoryRepository, ICategoryMapper categoryMapper)
    {
        _categoryRepository = categoryRepository;
        _categoryMapper = categoryMapper;
    }

    public async Task<CategoryDto> Create(CategoryDto request)
    {
        var category = _categoryMapper.FromCategoryDto(request);
        var stored = await _categoryRepository.SaveAsync(category);

        return _categoryMapper.ToCategoryDto(stored);
    }

    public async Task<CategoryDto> GetCategoryById(Guid id)
    {
        var stored = await _categoryRepository.GetCategoryByIdAsync(id) ??
            throw new NotFoundException(string.Format($"Nenhuma categoria encontrada."));

        return _categoryMapper.ToCategoryDto(stored);
    }

    public async Task<IEnumerable<CategoryDto>> GetCategories()
    {
        var stored = await _categoryRepository.GetCategoriesAsync() ??
            throw new NotFoundException(string.Format($"Nenhuma categoria encontrada."));

        return stored.Select(log => _categoryMapper.ToCategoryDto(log)).ToList();
    }
}
