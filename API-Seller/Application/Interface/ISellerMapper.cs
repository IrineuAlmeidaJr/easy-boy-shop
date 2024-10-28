using Application.DTO;
using Domain.Model;

namespace Application.Interface;

public interface ISellerMapper
{
    public Seller FromSellerRequest(SellerRequest SellerRequest);

    public SellerResponse ToSellerResponse(Seller Seller);
}
