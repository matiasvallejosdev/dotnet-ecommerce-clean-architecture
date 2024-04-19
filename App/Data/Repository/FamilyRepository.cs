using App.Contracts;
namespace App.Data;

using Microsoft.EntityFrameworkCore;

public class FamilyRepository : IFamilyRepository
{
    private readonly EntityDataContext _context;

    public FamilyRepository(EntityDataContext context)
    {
        _context = context;
    }

    public async Task<Family> CreateFamily(Family family)
    {
        family.CreatedAt = DateTime.Now;
        family.UpdatedAt = DateTime.Now;
        family.IsDown = false;
        await _context.Families.AddAsync(family);
        await _context.SaveChangesAsync();
        return family;
    }

    public async Task<Family> SoftDelete(int id)
    {
        var family = await _context.Families.FindAsync(id);
        if (family == null)
        {
            throw new KeyNotFoundException("Family not found.");
        }
        family.IsDown = true;
        family.DownAt = DateTime.Now;
        family.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return family;
    }

    public async Task<Family> HardDelete(int id)
    {
        var family = await _context.Families.FindAsync(id);
        if (family == null)
        {
            throw new KeyNotFoundException("Family not found.");
        }
        _context.Families.Remove(family);
        await _context.SaveChangesAsync();
        return family;
    }

    public async Task<Family> GetFamilyById(int id)
    {
        var family = await _context.Families.FindAsync(id);
        if (family == null)
        {
            throw new KeyNotFoundException("Family not found or has been deleted.");
        }
        return family;
    }

    public async Task<Family> UpdateFamily(Family family)
    {
        family.UpdatedAt = DateTime.Now;
        _context.Families.Update(family);
        await _context.SaveChangesAsync();
        return family;
    }

    public async Task<Family> UpFamily(int id)
    {
        var family = await _context.Families.FindAsync(id);
        if (family == null)
        {
            throw new KeyNotFoundException("Family not found.");
        }
        family.IsDown = false;
        family.DownAt = null;
        family.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return family;
    }

    public async Task<List<Family>> GetActivesFamilies()
    {
        var families = await _context.Families.Where(f => !f.IsDown).ToListAsync();
        return families;
    }
}