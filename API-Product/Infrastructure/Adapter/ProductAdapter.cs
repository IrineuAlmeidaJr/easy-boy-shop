using Domain.Model;
using Infrastructure.Interface;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Adapter;

public class ProductAdapter : IProductAdapter
{
    public ProductEntity ToProductEntity(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        CategoryId = product.Category.Id,
        SellerId = product.Seller.Id,
        Price = product.Price
    };
    public Product FromProductEntity(ProductEntity productEntity, SellerEntity sellerEntity, Category category, Stock? stock) => new(
        productEntity.Id,
        productEntity.Name,
        category,
        new Seller(
            sellerEntity.Id,
            sellerEntity.Name,
            sellerEntity.CNPJ,
            sellerEntity.Telefone,
            sellerEntity.Email,
            sellerEntity.Logradouro,
            sellerEntity.Cidade,
            sellerEntity.Estado
        ),
        stock,
        productEntity.Price
    );

    public Product FromProductEntity(ProductEntity productEntity) => new (
        productEntity.Id,
        productEntity.Name,
        null,
        null,
        null,
        productEntity.Price
    );
}