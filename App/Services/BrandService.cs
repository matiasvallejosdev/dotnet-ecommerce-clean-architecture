namespace App.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using App.Contracts;
using App.Data;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public Task<Brand> CreateBrand(BrandPostDto req)
    {
        var brand = new Brand()
        {
            Name = req.Name!
        };
        var result = _brandRepository.CreateBrand(brand);
        return result;   
    }

    public Task<Brand> DeleteBrand(int id, string type = "soft")
    {
        if (type == "hard")
        {
            return _brandRepository.HardDelete(id);
        }
        return _brandRepository.SoftDelete(id);
    }

    public Task<List<Brand>> GetAllBrands()
    {
        var brands = _brandRepository.GetActivesBrands();
        return brands;
    }

    public Task<Brand> GetBrandById(int id)
    {
        var brand = _brandRepository.GetBrandById(id);
        return brand;
    }

    public Task<Brand> UpBrand(int id)
    {
        var brand = _brandRepository.UpBrand(id);
        return brand;
    }

    public async Task<Brand> UpdateBrand(int id, BrandPatchDto req)
    {
        var brand = await _brandRepository.GetBrandById(id);
        brand.Name = req.Name!;
        var result = await _brandRepository.UpdateBrand(brand);
        return result;

    }
}