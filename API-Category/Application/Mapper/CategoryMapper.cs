using Application.DTO;
using Application.Interface;
using Domain.Model;

namespace Application.Mapper;

public class CategoryMapper : ICategoryMapper
{
    public Category FromCategoryDto(CategoryDto categoryDto) => new Category(categoryDto.Id, categoryDto.Name);

    public CategoryDto ToCategoryDto(Category category) => new() {
        Id = category.Id,
        Name = category.Name,
    };
}
