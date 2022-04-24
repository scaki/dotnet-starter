namespace CatalogService.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    void Save();
    void SaveAsync();
}