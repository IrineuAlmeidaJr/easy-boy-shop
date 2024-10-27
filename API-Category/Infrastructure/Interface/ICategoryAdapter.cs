using Infrastructure.Repository.Entity;
using Domain.Model;

namespace Infrastructure.Interface;

public interface ICategoryAdapter
{
    public CategoryEntity ToCategoryEntity(Category category);
    public Category FromCategoryEntity(CategoryEntity categoryEntity);
}
