namespace App.Contracts;
using App.Data;

public interface IBrandService
{
    Task<List<Brand>> GetAllBrands();
    Task<Brand> GetBrandById(int id);
    Task<Brand> CreateBrand(BrandPostDto req);
    Task<Brand> UpdateBrand(int id, BrandPatchDto req);
    Task<Brand> DeleteBrand(int id, string type);
    Task<Brand> UpBrand(int id);
}