namespace App.Data;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using App.Contracts;
using App.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly EntityDataContext _context;
    public ProductRepository(EntityDataContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateProduct(Product product)
    {
        var productExists = await _context.Products.Include(x => x.Brand).Include(x => x.Family).Include(x => x.Tags).FirstOrDefaultAsync(x => x.Code == product.Code);
        if (productExists != null)
        {
            productExists.IsDown = false;
            productExists.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            throw new BadHttpRequestException("Product already exists. We have updated the existing product. Now it is active.");
        }
        product.CreatedAt = DateTime.Now;
        product.UpdatedAt = DateTime.Now;
        product.IsDown = false;
        product.DownAt = null;
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public Task<List<Product>> GetActivesProducts()
    {
        return _context.Products.Include(x => x.Brand).Include(x => x.Family).Include(x => x.Tags).Where(x => !x.IsDown).ToListAsync();
    }

    public Task<List<Product>> GetInactivesProducts()
    {
        return _context.Products.Include(x => x.Brand).Include(x => x.Family).Include(x => x.Tags).Where(x => x.IsDown).ToListAsync();
    }

    public IQueryable<Product> GetFilteredQuery(ProductFilters filters)
    {
        var query = _context.Products.Include(x => x.Brand).Include(x => x.Family).Include(x => x.Tags).AsQueryable();
        if (filters.Name != null && filters.Name != "")
        {
            query = query.Where(x => x.Name.Contains(filters.Name));
        }
        if (filters.IdBrand != null && filters.IdBrand != 0)
        {
            query = query.Where(x => x.Brand != null && x.Brand.Id == filters.IdBrand);
        }
        if (filters.IdFamily != null && filters.IdFamily != 0)
        {
            query = query.Where(x => x.Family != null && x.Family.Id == filters.IdFamily);
        }
        if (filters.Code != null && filters.Code != "")
        {
            query = query.Where(x => x.Code.Contains(filters.Code));
        }
        if (filters.StockGreaterThan != null && filters.StockGreaterThan > 0)
        {
            query = query.Where(x => x.Stock > filters.StockGreaterThan);
        }
        if (filters.StockLessThan != null && filters.StockLessThan > 0)
        {
            query = query.Where(x => x.Stock < filters.StockLessThan);
        }
        if (filters.IsDown != null)
        {
            query = query.Where(x => x.IsDown == filters.IsDown);
        }
        return query;
    }

    public async Task<Product> GetProductById(int id)
    {
        var product = await _context.Products.Include(x => x.Brand).Include(x => x.Family).Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        if (product != null)
        {
            return product;
        }
        throw new KeyNotFoundException("Product not found.");
    }

    public async Task<Product> HardDelete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
        throw new KeyNotFoundException("Product not found.");
    }

    public async Task<Product> SoftDelete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            product.IsDown = true;
            product.DownAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return product;
        }
        throw new KeyNotFoundException("Product not found.");
    }

    public async Task<Product> UpProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            product.IsDown = false;
            product.DownAt = null;
            product.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return product;
        }
        throw new KeyNotFoundException("Product not found.");
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return product;
    }
}