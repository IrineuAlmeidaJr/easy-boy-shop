using Application.Client;
using Application.DTO.Seller;
using Domain.Exception;
using Outgoing.Http.Refit;
using Refit;
using System.Net;

namespace Outgoing.Http;

public class SellerClient : ISellerClient
{
    // Aqui não precisa de Mapper, pois, os dados virão sempre os mesmo do DTO
    private readonly ISellerRefitClient _sellerRefitClient;

    public SellerClient(ISellerRefitClient sellerRefitClient)
    {
        _sellerRefitClient = sellerRefitClient;
    }

    public async Task<SellerDto> CreateSellerAsync([Body] SellerDto request)
    {
        var sellerDto = await _sellerRefitClient.CreateAsync(request);

        return sellerDto;
    }

    public async Task<IEnumerable<SellerDto>> GetSellersAsync()
    {
        var customersDto = await _sellerRefitClient.GetSellersAsync();                  

        return customersDto;    
    }

    public async Task<SellerDto> GetSellerByIdAsync(Guid id)
    {
        return await _sellerRefitClient.GetSellerByIdAsync(id);
    }
}