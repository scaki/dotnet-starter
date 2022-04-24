using CatalogService.Domain.Common;

namespace CatalogService.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Price { get; set; }
    public string Stock { get; set; }
    public string[] Images { get; set; }
    public string Code { get; set; }

    public Category Category { get; set; }
}