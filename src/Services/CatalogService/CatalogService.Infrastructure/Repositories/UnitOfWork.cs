using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Infrastructure.Context;
using static System.GC;

namespace CatalogService.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    private CategoryRepository _categoryRepository;
    private ProductRepository _productRepository;

    public ICategoryRepository Category
    {
        get { return _categoryRepository ??= new CategoryRepository(_context); }
    }

    public IProductRepository Product
    {
        get { return _productRepository ??= new ProductRepository(_context); }
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool _disposed = false;

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        SuppressFinalize(this);
    }
}