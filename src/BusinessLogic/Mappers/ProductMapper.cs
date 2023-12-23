﻿using System.Collections.Immutable;
using Infrastructure.MongoDB.Products;

namespace BusinessLogic.Mappers;

public static class ProductMapper
{
    public static IReadOnlyCollection<ProductDto> MapListOfProducts(this IReadOnlyCollection<Product>? products)
    {
        return products is null ? ImmutableList<ProductDto>.Empty : products.Select(product => product.MapToProductDto()).ToImmutableList()!;
    }

    public static ProductDto? MapToProductDto(this Product? product)
    {
        return product is null ? default : new ProductDto
        {
            Category = product.Category,
            Name = product.Name,
            Description = product.Description,
            Id = product.ID,
            ImageUrl = product.Picture,
            IsActive = product.IsActive,
            Price = product.Price,
            Stock = product.Stock
        };
    }
}