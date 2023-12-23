namespace Infrastructure.MongoDB.Products;

public interface IProductRepository
{
    public Task<(IReadOnlyCollection<Product> Results, long TotalCount, int PageCount)> GetItemsAsync(SearchParams searchParams,
        CancellationToken cancellationToken);

    public Task<long> CountAsync(CancellationToken cancellationToken);
}