using Domain.Model;
using Infrastructure.Interface;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Adapter;

public class CategoryAdapter : ICategoryAdapter
{
    public Category FromCategoryEntity(CategoryEntity categoryEntity) => new Category(
        categoryEntity.Id,
        categoryEntity.Name
    );

    public CategoryEntity ToCategoryEntity(Category category) => new()
    {
        Id = category.Id,
        Name = category.Name
    };
}
