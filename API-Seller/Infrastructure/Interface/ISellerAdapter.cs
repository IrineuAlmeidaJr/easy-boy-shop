using Domain.Model;
using Infrastructure.Repository.Entity;

namespace Infrastructure.Interface;

public interface ISellerAdapter
{
    public SellerEntity ToSellerEntity(Seller seller);
    public Seller FromSellerEntity(SellerEntity sellerEntity);
}
