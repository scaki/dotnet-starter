using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsWithCategoryAsNoTracking();
    Task<Product> GetProductWithCategoryById(Guid productId);
}