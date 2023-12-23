using MongoDB.Entities;

namespace Infrastructure.MongoDB.Products;

public sealed class Product : Entity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string Category { get; set; } = null!;

    public string Picture { get; set; } = null!;

    public bool IsActive { get; set; }

    public int Stock { get; set; }
}