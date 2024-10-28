using Application.DTO;

namespace Application.Interface;

public interface ICategoryServices
{
    Task<CategoryDto> Create(CategoryDto request);
    Task<IEnumerable<CategoryDto>> GetCategories();
    Task<CategoryDto> GetCategoryById(Guid id);
}
