using Domain.Model;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Interface;

public interface IProductAdapter
{
    ProductEntity ToProductEntity(Product product);
    Product FromProductEntity(ProductEntity productEntity, SellerEntity sellerEntity, Category category, Stock? stock);
    Product FromProductEntity(ProductEntity productEntity);
}
