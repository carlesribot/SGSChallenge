using Infrastructure.Interfaces;
using MongoDB.Entities;

namespace Infrastructure.MongoDB.Products;

public sealed class ProductRepository : IProductRepository
{
    public async Task<(IReadOnlyCollection<MongoDBDriver.Products.Product> Results, long TotalCount, int PageCount)> GetItemsAsync(
        SearchParams searchParams, CancellationToken cancellationToken)
    {
        var query = DB.PagedSearch<Product, Product>();

        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
            _ = query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();

        query = searchParams.OrderBy switch
        {
            "name-asc" => query.Sort(x => x.Ascending(a => a.Name)),
            "name-desc" => query.Sort(x => x.Descending(a => a.Name)),
            "regularPrice-asc" => query.Sort(x => x.Ascending(a => a.Price)),
            "regularPrice-desc" => query.Sort(x => x.Descending(a => a.Price)),
            "category-asc" => query.Sort(x => x.Ascending(a => a.Category)),
            "category-desc" => query.Sort(x => x.Descending(a => a.Category)),
            _ => query.Sort(x => x.Ascending(a => a.Name))
        };

        query = searchParams.FilteredBy switch
        {
            "inactive" => query.Match(x => !x.IsActive || x.Price == 0),
            "active" => query.Match(x => x.IsActive && x.Price > 0),
            _ => query.Match(x => true)
        };

        if (!string.IsNullOrEmpty(searchParams.Category))
        {
            query.Match(x => x.Category == searchParams.Category);
        }

        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        var result = await query.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        // return (result.Results, result.TotalCount, result.PageCount);
        return default;
    }

    public async Task<long> CountAsync(CancellationToken cancellationToken)
    {
        var count = await DB.Collection<Product>().CollectionCount(cancellationToken).ConfigureAwait(false);

        return count;
    }
}