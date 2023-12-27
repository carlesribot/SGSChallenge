using System.ComponentModel.DataAnnotations;

namespace Infrastructure.MongoDB.Products;

public sealed class SearchParams
{
    [MaxLength(50, ErrorMessage = "The Search term has to be a maximum of 50 characters")]
    public string SearchTerm { get; set; } = string.Empty;

    public int PageNumber { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "The Page Number has to be between 1 and 100")]
    public int PageSize { get; set; } = 50;

    public string OrderBy { get; set; } = string.Empty;

    public string FilteredBy { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;
}