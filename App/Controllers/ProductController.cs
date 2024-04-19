namespace App.Controllers;

using App.Contracts;
using App.Services;
using App.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductPostDto req)
    {
        try
        {
            var product = await _productService.CreateProduct(req);
            var response = MapProductToDetailDto(product);
            return Ok(response);
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsList(
     [FromQuery] string? code = "",
     [FromQuery] string? name = "",
     [FromQuery] int stockGreaterThan = -1,
     [FromQuery] int stockLessThan = -1,
     [FromQuery] int brandId = 0,
     [FromQuery] int familyId = 0,
     [FromQuery] string? status = "",
     [FromQuery] string details = "normal")
    {
        try
        {
            var products = await _productService.GetAllProducts(code, name, stockGreaterThan, stockLessThan, brandId, familyId, status);
            if (details == "full")
            {
                var fullProducts = products.Select(p => MapProductToDetailDto(p)).ToList();
                return Ok(fullProducts);
            }
            var productList = products.Select(p => MapProductToDto(p)).ToList();
            return Ok(productList);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            var response = MapProductToDetailDto(product);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id, [FromQuery] string type = "")
    {
        try
        {
            var product = await _productService.DeleteProduct(id, type);
            if (product == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("{id}/up")]
    public async Task<IActionResult> UpProduct(int id)
    {
        try
        {
            var product = await _productService.UpProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductPatchDto req)
    {
        try
        {
            var product = await _productService.UpdateProduct(id, req);
            if (product == null)
            {
                return NotFound();
            }
            var response = MapProductToDetailDto(product);
            return Ok(response);
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [NonAction]
    public ProductGetDetailDto MapProductToDetailDto(Product product)
    {
        return new ProductGetDetailDto()
        {
            Id = product.Id,
            Code = product.Code,
            Name = product.Name,
            Description = product.Description,
            PriceCost = product.PriceCost,
            PriceSale = product.PriceSale,
            Stock = product.Stock,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            IsDown = product.IsDown,
            DownAt = product.DownAt,
            Brand = new BrandGetDto()
            {
                Id = product.Brand != null ? product.Brand.Id : 0,
                Name = product.Brand != null ? product.Brand.Name : "No brand"
            },
            Family = new FamilyGetDto()
            {
                Id = product.Family != null ? product.Family.Id : 0,
                Name = product.Family != null ? product.Family.Name : "No family"
            },
            Tags = product.Tags?.Select(t => new TagGetDto()
            {
                Id = t.Id,
                Name = t.Name
            }).ToList() ?? new List<TagGetDto>()
        };
    }

    [NonAction]
    public ProductGetDto MapProductToDto(Product product)
    {
        return new ProductGetDto()
        {
            Id = product.Id,
            Code = product.Code,
            Name = product.Name,
            Stock = product.Stock,
            Price = product.PriceSale,
            BrandName = product.Brand?.Name ?? "No brand",
            FamilyName = product.Family?.Name ?? "No family",
        };
    }
}
