namespace App.Data;

using App.Contracts;
using App.Data;

using Microsoft.EntityFrameworkCore;

public class BrandRepository : IBrandRepository
{
    private readonly EntityDataContext _context;
    public BrandRepository(EntityDataContext context)
    {
        _context = context;
    }
    
    public async Task<Brand> CreateBrand(Brand brand)
    {
        brand.CreatedAt = DateTime.Now;
        brand.UpdatedAt = DateTime.Now;
        brand.IsDown = false;
        await _context.Brands.AddAsync(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand> SoftDelete(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand == null)
        {
            throw new KeyNotFoundException("Brand not found.");
        }
        brand.IsDown = true;
        brand.DownAt = DateTime.Now;
        brand.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand> HardDelete(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand == null)
        {
            throw new KeyNotFoundException("Brand not found.");
        }
        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task<List<Brand>> GetActivesBrands()
    {
        var brands = await _context.Brands.Where(brand => !brand.IsDown).ToListAsync();
        return brands;
    }

    public async Task<Brand> GetBrandById(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand == null)
        {
            throw new KeyNotFoundException("Brand not found or has been deleted.");
        }
        return brand;
    }

    public async Task<Brand> UpBrand(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand == null)
        {
            throw new KeyNotFoundException("Brand not found.");
        }
        brand.IsDown = false;
        brand.UpdatedAt = DateTime.Now;
        brand.DownAt = null;
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand> UpdateBrand(Brand brand)
    {
        brand.UpdatedAt = DateTime.Now;
        _context.Brands.Update(brand);
        await _context.SaveChangesAsync();
        return brand;
    }
}