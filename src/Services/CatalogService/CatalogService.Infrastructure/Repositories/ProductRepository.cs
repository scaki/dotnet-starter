using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Product>> GetProductsWithCategoryAsNoTracking()
        {
            var products = Context.Products.AsNoTracking().Include(p => p.Category);
            return Task.FromResult<IEnumerable<Product>>(products);
        }

        public Task<Product> GetProductWithCategoryById(Guid productId)
        {
            return Context.Products.Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == productId);
        }
    }
}