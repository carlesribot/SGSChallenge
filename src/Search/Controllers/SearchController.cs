using BusinessLogic.Products;
using Infrastructure.MongoDB.Products;
using Microsoft.AspNetCore.Mvc;

namespace Search_Api.Controllers;

[ApiController]
[Route("api/search")]
public sealed class SearchController : ControllerBase
{
    public async Task<ActionResult<List<Product>>> SearchProducts(
        [FromQuery] SearchParams searchParams,
        [FromServices] ProductService productService,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await productService.GetProductsAsync(searchParams, cancellationToken).ConfigureAwait(false);

        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}



