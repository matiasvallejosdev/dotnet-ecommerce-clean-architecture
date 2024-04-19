namespace App.Contracts;

using App.Data;

public interface IProductRepository
{
    IQueryable<Product> GetFilteredQuery(ProductFilters filters);
    Task<List<Product>> GetActivesProducts();
    Task<List<Product>> GetInactivesProducts();
    Task<Product> GetProductById(int id);
    Task<Product> CreateProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task<Product> SoftDelete(int id);
    Task<Product> HardDelete(int id);
    Task<Product> UpProduct(int id);
}