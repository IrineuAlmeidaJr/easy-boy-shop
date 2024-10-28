using Application.DTO;
using Application.Interface;
using Domain.Interface;
using Domain.Exception;

namespace Application.Service;

public class SellerServices : ISellerServices
{
    private ISellerRepository _sellerRepository;
    private ISellerMapper _sellerMapper;

    public SellerServices(ISellerRepository sellerRepository, ISellerMapper sellerMapper)
    {
        _sellerRepository = sellerRepository;
        _sellerMapper = sellerMapper;
    }
   
    public async Task<SellerResponse> Create(SellerRequest request)
    {
        var seller = _sellerMapper.FromSellerRequest(request);
        var stored = await _sellerRepository.SaveAsync(seller);

        return _sellerMapper.ToSellerResponse(stored);
    }

    public async Task<SellerResponse> GetSellerById(Guid id)
    {
        var stored = await _sellerRepository.GetSellerByIdAsync(id) ??
            throw new NotFoundException($"Vendedor com ID {id} não encontrado");

        return _sellerMapper.ToSellerResponse(stored);
    }

    public async Task<IEnumerable<SellerResponse>> GetSellers()
    {
        var stored = await _sellerRepository.GetSellersAsync() ??
            throw new NotFoundException($"Nenhum Cliente encontrado");

        return stored.Select(log => _sellerMapper.ToSellerResponse(log)).ToList();
    }

    public async Task Delete(Guid id)
    {
     var seller = await _sellerRepository.GetSellerByIdAsync(id);
        if (seller == null)
        {
            throw new NotFoundException($"Vendedor com ID {id} não encontrado");
        }

      
        await _sellerRepository.DeleteAsync(id);
    }
}
