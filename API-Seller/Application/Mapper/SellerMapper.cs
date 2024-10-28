using Application.DTO;
using Application.Interface;
using Domain.Model;

namespace Application.Mapper;

public class SellerMapper : ISellerMapper
{
    public Seller FromSellerRequest(SellerRequest sellerRequest) => new(
        sellerRequest.Id,
        sellerRequest.Name,
        sellerRequest.Cnpj,
        sellerRequest.Telefone,
        sellerRequest.Email,
        sellerRequest.Logradouro,
        sellerRequest.Cidade,
        sellerRequest.Estado
    );

    public SellerResponse ToSellerResponse(Seller seller) => new()
    {
        Id = seller.Id,
        Name = seller.Name,
        Cnpj = seller.CNPJ,
        Telefone = seller.Telefone,
        Email = seller.Email,
        Logradouro = seller.Logradouro,
        Cidade = seller.Cidade,
        Estado = seller.Estado,
    };
}
