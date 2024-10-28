using Cassandra;
using Domain.Exception;
using Domain.Interface;
using Domain.Model;
using Infrastructure.Interface;

namespace Infrastructure.Repository;

public class SellerRepository : ISellerRepository
{
    private readonly ISellerScyllaRepository _sellerRepository;
    private readonly ISellerAdapter _sellerAdapter;

    public SellerRepository(ISellerScyllaRepository sellerRepository, ISellerAdapter sellerAdapter)
    {
        _sellerRepository = sellerRepository;
        _sellerAdapter = sellerAdapter;
    }

    public async Task<Seller> SaveAsync(Seller seller)
    {
        var entity = _sellerAdapter.ToSellerEntity(seller);
        var storedEntity = await _sellerRepository.SaveAsync(entity);
        var storedCustomer = _sellerAdapter.FromSellerEntity(storedEntity);

        return storedCustomer;
    }

    public async Task<Seller?> GetSellerByIdAsync(Guid id)
    {
        var storedEntity = await _sellerRepository.GetByIdAsync(id);

        if (storedEntity == null)
            return null;               

        return _sellerAdapter.FromSellerEntity(storedEntity);
    }

    public async Task<IEnumerable<Seller>?> GetSellersAsync()
    {
        var storedEntity = await _sellerRepository.GetSellers();

        if (storedEntity == null)
            return null;

        var storedCustomers = storedEntity.Select(stored => _sellerAdapter.FromSellerEntity(stored)).ToList();

        return storedCustomers;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {        
        return await _sellerRepository.DeleteAsync(id);
    }
}
