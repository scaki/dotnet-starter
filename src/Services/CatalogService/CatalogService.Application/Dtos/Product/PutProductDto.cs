namespace CatalogService.Application.Dtos.Product;

public class PutProductDto
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string[] Images { get; set; }
    public string Code { get; set; }
    public int Stock { get; set; }
    public PutProductsCategory Category { get; set; }
}

public class PutProductsCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}