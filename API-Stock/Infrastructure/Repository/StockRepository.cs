using Domain.Interface;
using Domain.Model;
using Infrastructure.Interface;

namespace Infrastructure.Repository;

public class StockRepository : IStockRepository
{
    private readonly IStockScyllaRepository _stockScyllaRepository;

    public StockRepository(IStockScyllaRepository stockScyllaRepository)
    {
        _stockScyllaRepository = stockScyllaRepository;
    }

    public async Task<Stock> SaveAsync(Stock stock)
    {
        var stored = await _stockScyllaRepository.SaveAsync(stock);

        return stored;
    }

    public Task<Stock?> GetStockByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Stock>?> GetStocksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteStockAsync(Guid id)
    {
        throw new NotImplementedException();
    }  

   

    //public async Task<Stock> SaveAsync(Stock stock)
    //{
    //    var storedEntity = await _productScyllaRepository.SaveAsync(entity);
    //    var storedProduct = _productAdapter.FromProductEntity(storedEntity);

    //    return storedProduct;
    //}

    //public async Task<Product?> GetProductByIdAsync(Guid id)
    //{
    //    var storedProductEntity = await _productScyllaRepository.GetByIdAsync(id);

    //    if (storedProductEntity == null)  
    //        return null;

    //    return await GetFullProduct(storedProductEntity);
    //}

    //public async Task<IEnumerable<Product>?> GetProductsAsync()
    //{
    //    var storedEntity = await _productScyllaRepository.GetProducts();

    //    if (storedEntity == null)
    //        return null;

    //    var storedProductsTasks = storedEntity.Select(async stored => await GetFullProduct(stored));
    //    var storedProducts = await Task.WhenAll(storedProductsTasks);

    //    return storedProducts.ToList();
    //}

    //public async Task<bool> DeleteProductAsync(Guid id)
    //{
    //    return await _productScyllaRepository.DeleteAsync(id);
    //}

    //private async Task<Product> GetFullProduct(ProductEntity productEntity)
    //{
    //    // Buscar Seller
    //    var storedSellerEntity = await _sellerScyllaRepository.GetByIdAsync(productEntity.SellerId);
    //    if (storedSellerEntity == null)
    //        return null;

    //    // Buscar Category
    //    var storedCategory = await _categoryScyllaRepository.GetCategoryByIdAsync(productEntity.CategoryId);
    //    if (storedCategory == null)
    //        return null;

    //    // Buscar Stock
    //    var storedStock = await _stockScyllaRepository.GetStockByProductIdAsync(productEntity.Id);

    //    return _productAdapter.FromProductEntity(productEntity, storedSellerEntity, storedCategory, storedStock);
    //}
}
