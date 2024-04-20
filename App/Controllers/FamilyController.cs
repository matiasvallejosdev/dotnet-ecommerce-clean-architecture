namespace App.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using App.Contracts;
using App.Services;

[ApiController]
[Route("api/[controller]")]
public class FamilyController : ControllerBase
{
    private readonly IFamilyService _familyService;
    public FamilyController(IFamilyService familyService)
    {
        _familyService = familyService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFamily([FromBody] FamilyPostDto request)
    {
        try
        {
            var family = await _familyService.CreateFamily(request);
            var res = new FamilyGetDto()
            {
                Id = family.Id,
                Name = family.Name
            };
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetFamiliesList([FromQuery] string? details = "")
    {
        try
        {
            var families = await _familyService.GetAllFamilies();
            if (!string.IsNullOrEmpty(details) && details == "full")
            {
                var resDetails = families.Select(f => new FamilyGetDetailDto()
                {
                    Id = f.Id,
                    Name = f.Name,
                    CreatedAt = f.CreatedAt,
                    UpdatedAt = f.UpdatedAt,
                    IsDown = f.IsDown,
                    DownAt = f.DownAt
                }).ToList();
                return Ok(resDetails);
            }

            var res = families.Select(f => new FamilyGetDto()
            {
                Id = f.Id,
                Name = f.Name
            }).ToList();

            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFamilyById(int id)
    {
        try
        {
            var family = await _familyService.GetFamilyById(id);
            var res = new FamilyGetDetailDto()
            {
                Id = family.Id,
                Name = family.Name,
                CreatedAt = family.CreatedAt,
                UpdatedAt = family.UpdatedAt,
                IsDown = family.IsDown,
                DownAt = family.DownAt
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFamily(int id, [FromQuery] string type = "soft")
    {
        try
        {
            var family = await _familyService.DeleteFamily(id, type);
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
    public async Task<IActionResult> UpFamily(int id)
    {
        try
        {
            var family = await _familyService.UpFamily(id);
            return Ok($"Family with ID {id} is up again.");
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
    public async Task<IActionResult> UpdateFamily(int id, [FromBody] FamilyPatchDto request)
    {
        try
        {
            var family = await _familyService.UpdateFamily(id, request);
            var res = new FamilyGetDetailDto()
            {
                Id = family.Id,
                Name = family.Name,
                CreatedAt = family.CreatedAt,
                UpdatedAt = family.UpdatedAt,
                IsDown = family.IsDown,
                DownAt = family.DownAt
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
}