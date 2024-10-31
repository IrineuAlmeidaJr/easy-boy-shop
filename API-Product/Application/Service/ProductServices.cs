using Application.DTO;
using Application.Interface;
using Domain.Interface;
using Domain.Exception;
using Domain.Model;
using Domain.Event;

namespace Application.Service;

public class ProductServices : IProductServices
{
    private IProductRepository _productRepository;
    private IProductCreatedEventPublisher _publisher;
    private IProductMapper _productMapper;

    public ProductServices(IProductRepository productRepository, IProductCreatedEventPublisher publisher, IProductMapper productMapper)
    {
        _productRepository = productRepository;
        _publisher = publisher;
        _productMapper = productMapper;
    }

    public async Task<ProductDto> Create(ProductRequest request)
    {
        var product = _productMapper.FromProductRequest(request);
        var stored = await _productRepository.SaveAsync(product);

        if (stored == null)
            throw new Domain.Exception.InvalidOperationException("erro interno ao inserir/alterar o produto");

        await PublishEvent(request, stored);

        return _productMapper.ToProductDto(stored);
    }

    public async Task<ProductResponse> GetProductById(Guid id)
    {
        var stored = await _productRepository.GetProductByIdAsync(id) ??
            throw new NotFoundException($"Produto de ID {id} não encontrado");

        return _productMapper.ToProductResponse(stored);
    }

    public async Task<IEnumerable<ProductResponse>> GetProducts()
    {
        var stored = await _productRepository.GetProductsAsync() ??
            throw new NotFoundException($"Nenhum Produto encontrado");

        return stored.Select(log => _productMapper.ToProductResponse(log)).ToList();
    }

    private async Task PublishEvent(ProductRequest productRequest, Product product)
    {
        var generatedEvent = new ProductCreatedEvent
        {
            ProductId = product.Id,
            Quantity = productRequest.Quantity
        };

        await _publisher.PublishAsync(generatedEvent);
    }
}
