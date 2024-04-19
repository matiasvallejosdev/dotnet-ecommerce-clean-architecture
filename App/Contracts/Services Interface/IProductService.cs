namespace App.Contracts;

using App.Data;

public interface IProductService
{
    Task<List<Product>> GetAllProducts(string? code, string? name, int? stockGreaterThan, int? stockLessThan,
    int? idBrand, int? idFamily, string? status);
    Task<Product> GetProductById(int id);
    Task<Product> CreateProduct(ProductPostDto req);
    Task<Product> UpdateProduct(int id, ProductPatchDto req);
    Task<Product> DeleteProduct(int id, string type);
    Task<Product> UpProduct(int id);
}