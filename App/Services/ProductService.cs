namespace App.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using App.Contracts;
using App.Data;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IFamilyRepository _familyRepository;
    private readonly ITagRepository _tagRepository;

    public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, 
    IFamilyRepository familyRepository, ITagRepository tagRepository)
    {
        _productRepository = productRepository;
        _familyRepository = familyRepository;
        _brandRepository = brandRepository;
        _tagRepository = tagRepository;
    }

    public async Task<List<Product>> GetAllProducts(string? code, string? name, int? stockGreaterThan, 
    int? stockLessThan, int? idBrand, int? idFamily, string? status)
    {
        var isDown = string.IsNullOrEmpty(status) ? (bool?)null : status == "down";
        var filters = new ProductFilters()
        {
            Code = code,
            Name = name,
            StockGreaterThan = stockGreaterThan,
            StockLessThan = stockLessThan,
            IdBrand = idBrand,
            IdFamily = idFamily,
            IsDown = isDown,
        };
        var products = _productRepository.GetFilteredQuery(filters);
        return await products.OrderByDescending(p => p.CreatedAt).ToListAsync();
    }

    public async Task<Product> CreateProduct(ProductPostDto req)
    {
        var brand = await _brandRepository.GetBrandById(req.BrandId);
        var family = await _familyRepository.GetFamilyById(req.FamilyId);

        var tags = new List<Tag>();
        if (req.Tags != null)
        {
            foreach (var name in req.Tags)
            {
                var nameNormalized = name.Replace(" ", "-").ToLower();

                // Check if the tag already exists
                var tag = await _tagRepository.GetTagByName(nameNormalized);

                // If tag does not exist, create and add to repository
                if (tag == null)
                {
                    tag = new Tag() { Name = nameNormalized };
                    tag = await _tagRepository.CreateTag(tag);
                }

                tags.Add(tag);
            }
        }

        var product = new Product()
        {
            Code = req.Code!,
            Name = req.Name!,
            Description = req.Description!,
            PriceCost = req.PriceCost,
            PriceSale = req.PriceSale,
            Stock = req.Stock,
            Brand = brand,
            Family = family,
            Tags = tags
        };

        var result = await _productRepository.CreateProduct(product);
        return result;
    }


    public async Task<Product> GetProductById(int id)
    {
        var product = await _productRepository.GetProductById(id);
        return product;
    }

    public async Task<Product> DeleteProduct(int id, string type)
    {
        var result = type == "hard" ? await _productRepository.HardDelete(id) : await _productRepository.SoftDelete(id);
        return result;
    }

    public async Task<Product> UpProduct(int id)
    {
        var result = await _productRepository.UpProduct(id);
        return result;
    }

    public async Task<Product> UpdateProduct(int id, ProductPatchDto req)
    {
        var product = await _productRepository.GetProductById(id);
        
        product.Name = req.Name ?? product.Name;
        product.Description = req.Description ?? product.Description;
        product.PriceCost = req.PriceCost ?? product.PriceCost;
        product.PriceSale = req.PriceSale ?? product.PriceSale;
        product.Stock = req.Stock ?? product.Stock;

        var family = req.FamilyId != null ? await _familyRepository.GetFamilyById(req.FamilyId.Value) : null;
        if (family != null)
        {
            product.Family = family;
        }
        var brand = req.BrandId != null ? await _brandRepository.GetBrandById(req.BrandId.Value) : null;
        if (brand != null)
        {
            product.Brand = brand;
        }

        if(req.Stock != null && req.Stock < 0)
        {
            throw new BadHttpRequestException("Stock cannot be negative");
        }

        var tags = product.Tags ?? new List<Tag>();
        if (req.Tags != null)
        {
            foreach (var name in req.Tags)
            {
                var tag = new Tag()
                {
                    Name = name
                };
                var createdTag = await _tagRepository.CreateTag(tag);
                tags.Add(createdTag);
            }
        }
        product.Tags = tags;

        var result = await _productRepository.UpdateProduct(product);
        return result;
    }

}