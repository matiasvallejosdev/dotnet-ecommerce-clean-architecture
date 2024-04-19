namespace App.Contracts;

using App.Data;

public interface IBrandRepository
{
    Task<List<Brand>> GetActivesBrands();
    Task<Brand> GetBrandById(int id);
    Task<Brand> CreateBrand(Brand brand);
    Task<Brand> UpdateBrand(Brand brand);
    Task<Brand> SoftDelete(int id);
    Task<Brand> HardDelete(int id);
    Task<Brand> UpBrand(int id);
}