using Application.DTO;
using Domain.Model;

namespace Application.Interface;
public interface ICategoryMapper
{
    public Category FromCategoryDto(CategoryDto categoryDto);

    public CategoryDto ToCategoryDto(Category category);
}
