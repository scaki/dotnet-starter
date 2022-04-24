using CatalogService.Domain.Common;

namespace CatalogService.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public string[] Images { get; set; }
    public string Code { get; set; }

    public Category Category { get; set; }
}