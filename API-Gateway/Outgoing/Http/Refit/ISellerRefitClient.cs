using Application.DTO.Customer.Request;
using Application.DTO.Seller;
using Refit;

namespace Outgoing.Http.Refit;

public interface ISellerRefitClient
{

    [Post("/api/v1/seller")]
    Task<SellerDto> CreateAsync([Body] SellerDto request);

    [Get("/api/v1/seller/{id}")]
    Task<SellerDto> GetSellerByIdAsync(Guid? id);

    [Get("/api/v1/seller/all")]
    Task<IEnumerable<SellerDto>> GetSellersAsync();

}
