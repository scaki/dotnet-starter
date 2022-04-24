namespace CatalogService.Application.Dtos.Product;

public class PostProductDto
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string[] Images { get; set; }
    public string Code { get; set; }
    public int Stock { get; set; }
    public PostProductsCategory Category { get; set; }
}

public class PostProductsCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}