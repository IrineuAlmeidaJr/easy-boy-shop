using Application.DTO.Seller;

namespace Application.Client;

public interface ISellerClient
{
    Task<IEnumerable<SellerDto>> GetSellersAsync();
    Task<SellerDto> GetSellerByIdAsync(Guid id);
    Task<SellerDto> CreateSellerAsync(SellerDto request);
}
