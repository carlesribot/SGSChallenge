namespace Infrastructure.DTO;

public sealed class ProductDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public string Category { get; set; }

    public bool IsActive { get; set; }

    //public IEnumerable<DiscountDto> Discounts { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

}