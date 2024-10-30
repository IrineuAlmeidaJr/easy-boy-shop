using Domain.Exception;
using Domain.Interface;
using Domain.Model;
using Infrastructure.Interface;
using Infrastructure.Repository.Entity;
using Infrastructure.Repository.Scylla;

namespace Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IProductScyllaRepository _productScyllaRepository;
    private readonly ICategoryScyllaRepository _categoryScyllaRepository;
    private readonly ISellerScyllaRepository _sellerScyllaRepository;
    private readonly IStockScyllaRepository _stockScyllaRepository;
    private readonly IProductAdapter _productAdapter;
    private readonly ISellerAdapter _sellerAdapter;

    public ProductRepository(IProductScyllaRepository productRepository, ICategoryScyllaRepository categoryScyllaRepository, 
        ISellerScyllaRepository sellerScyllaRepository, IStockScyllaRepository stockScyllaRepository, ISellerAdapter sellerAdapter,
        IProductAdapter productAdapter)
    {
        _productScyllaRepository = productRepository;
        _categoryScyllaRepository = categoryScyllaRepository;
        _sellerScyllaRepository = sellerScyllaRepository;
        _stockScyllaRepository = stockScyllaRepository;
        _productAdapter = productAdapter;
        _sellerAdapter = sellerAdapter;
    }

    public async Task<Product> SaveAsync(Product product)
    {
        var entity = _productAdapter.ToProductEntity(product);
        var storedEntity = await _productScyllaRepository.SaveAsync(entity);
        var storedProduct = _productAdapter.FromProductEntity(storedEntity);

        return storedProduct;
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        var storedProductEntity = await _productScyllaRepository.GetByIdAsync(id);

        if (storedProductEntity == null)  
            return null;

        return await GetFullProduct(storedProductEntity);
    }

    public async Task<IEnumerable<Product>?> GetProductsAsync()
    {
        var storedEntity = await _productScyllaRepository.GetProducts();

        if (storedEntity == null)
            return null;

        var storedProductsTasks = storedEntity.Select(async stored => await GetFullProduct(stored));
        var storedProducts = await Task.WhenAll(storedProductsTasks);

        return storedProducts.ToList();
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        return await _productScyllaRepository.DeleteAsync(id);
    }

    private async Task<Product> GetFullProduct(ProductEntity productEntity)
    {
        // Buscar Seller
        var storedSellerEntity = await _sellerScyllaRepository.GetByIdAsync(productEntity.SellerId);
        if (storedSellerEntity == null)
            return null;

        // Buscar Category
        var storedCategory = await _categoryScyllaRepository.GetCategoryByIdAsync(productEntity.CategoryId);
        if (storedCategory == null)
            return null;

        // Buscar Stock
        var storedStock = await _stockScyllaRepository.GetStockByProductIdAsync(productEntity.Id);

        return _productAdapter.FromProductEntity(productEntity, storedSellerEntity, storedCategory, storedStock);
    }
}
