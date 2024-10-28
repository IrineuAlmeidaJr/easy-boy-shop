using Application.DTO;

namespace Application.Interface;

public interface ISellerServices
{
    Task<SellerResponse> Create(SellerRequest request);
    Task<IEnumerable<SellerResponse>> GetSellers();
    Task<SellerResponse> GetSellerById(Guid id);
    Task Delete(Guid id);
}
