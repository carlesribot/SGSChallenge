using System.ComponentModel.DataAnnotations;

namespace Infrastructure.MongoDBDriver;

public sealed class MongoDbOption
{
    public const string Name = "MongoDBDriver";

    [Required]
    public string ConnectionString { get; set; }

    [Required]
    public string DatabaseName { get; set; }
}