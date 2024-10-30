using Domain.Model;
using Infrastructure.Interface;
using Infrastructure.Repository.Entity;
using System.Xml.Linq;

namespace Infrastructure.Adapter;

public class SellerAdapter : ISellerAdapter
{
    public Seller FromSellerEntity(SellerEntity sellerEntity) => new Seller(
        sellerEntity.Id,
        sellerEntity.Name,
        sellerEntity.CNPJ,
        sellerEntity.Telefone,
        sellerEntity.Email,
        sellerEntity.Logradouro,
        sellerEntity.Cidade,
        sellerEntity.Estado                
    );

    public SellerEntity ToSellerEntity(Seller seller) => new()
    {
        Id = seller.Id,
        Name = seller.Name,
        CNPJ = seller.CNPJ,
        Telefone = seller.Telefone,
        Email = seller.Email,
        Logradouro = seller.Logradouro,
        Cidade = seller.Cidade,
        Estado = seller.Estado,
    };
}
