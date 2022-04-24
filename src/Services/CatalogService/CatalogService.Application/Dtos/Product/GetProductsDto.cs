namespace CatalogService.Application.Dtos.Product;

public class GetProductsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string[] Images { get; set; }
    public string Code { get; set; }
    public GetProductsCategory Category { get; set; }
}

public class GetProductsCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}