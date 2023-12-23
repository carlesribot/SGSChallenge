using BusinessLogic.Mappers;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Infrastructure.MongoDB.Products;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Products;

public sealed class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
        _logger = logger;
    }

    public async Task<(IReadOnlyCollection<ProductDto> Results, long TotalCount, int PageCount)> GetProductsAsync(SearchParams searchParams, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _productRepository.GetItemsAsync(searchParams, cancellationToken).ConfigureAwait(false);

            return (result.Results.MapListOfProducts(), result.TotalCount, result.PageCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Getting items from the repo");
            throw;
        }
    }
}




