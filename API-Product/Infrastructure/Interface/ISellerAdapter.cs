using Domain.Model;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Interface;

public interface ISellerAdapter
{
    SellerEntity ToSellerEntity(Seller seller);
    Seller FromSellerEntity(SellerEntity sellerEntity);
}
