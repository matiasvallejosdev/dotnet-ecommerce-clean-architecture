namespace App.Controllers;

using App.Services;
using App.Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    public readonly IBrandService _brandService;
    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBrand([FromBody] BrandPostDto req)
    {
        try
        {
            var brand = await _brandService.CreateBrand(req);
            var response = new BrandGetDto()
            {
                Id = brand.Id,
                Name = brand.Name
            };
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetBrandsList([FromQuery] string? details = "")
    {
        try
        {
            var brands = await _brandService.GetAllBrands();
            if (!string.IsNullOrEmpty(details) && details == "full")
            {
                var fullBrands = brands.Select(b => new BrandGetDetailDto()
                {
                    Id = b.Id,
                    Name = b.Name,
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt,
                    IsDown = b.IsDown,
                    DownAt = b.DownAt
                }).ToList();
                return Ok(fullBrands);
            }
            var brandList = brands.Select(b => new BrandGetDto()
            {
                Id = b.Id,
                Name = b.Name
            }).ToList();
            return Ok(brandList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrand(int id)
    {
        try
        {
            var brand = await _brandService.GetBrandById(id);
            var res = new BrandGetDetailDto()
            {
                Id = brand.Id,
                Name = brand.Name,
                CreatedAt = brand.CreatedAt,
                UpdatedAt = brand.UpdatedAt,
                IsDown = brand.IsDown,
                DownAt = brand.DownAt
            };
            return Ok(res);
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
    public async Task<IActionResult> UpdateBrand(int id, [FromBody] BrandPatchDto req)
    {
        try
        {
            var brand = await _brandService.UpdateBrand(id, req);
            var updatedResponse = new BrandGetDto()
            {
                Id = brand.Id,
                Name = brand.Name
            };
            return Ok(updatedResponse);
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
    public async Task<IActionResult> DeleteBrand(int id, [FromQuery] string type = "soft")
    {
        try
        {
            var brand = await _brandService.DeleteBrand(id, type);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("{id}/up")]
    public async Task<IActionResult> UpBrand(int id)
    {
        try
        {
            var brand = await _brandService.UpBrand(id);
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
}
