namespace App.Services;

using App.Data;
using App.Contracts;

using System.Threading.Tasks;
using System.Collections.Generic;

public class FamilyService : IFamilyService
{
    private readonly IFamilyRepository _familyRepository;

    public FamilyService(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public async Task<Family> CreateFamily(FamilyPostDto req)
    {
        var family = new Family()
        {
            Name = req.Name!
        };
        var result = await _familyRepository.CreateFamily(family);
        return result;
    }

    public async Task<List<Family>> GetAllFamilies()
    {
        var families = await _familyRepository.GetActivesFamilies();
        return families;
    }

    public async Task<Family> GetFamilyById(int id)
    {
        var family = await _familyRepository.GetFamilyById(id);
        return family;
    }

    public async Task<Family> UpdateFamily(int id, FamilyPatchDto req)
    {
        var family = await _familyRepository.GetFamilyById(id);
        family.Name = req.Name!;
        var result = await _familyRepository.UpdateFamily(family);
        return result;
    }

    public async Task<Family> UpFamily(int id)
    {
        var family = await _familyRepository.UpFamily(id);
        return family;
    }

    public async Task<Family> DeleteFamily(int id, string type = "soft")
    {
        if (type == "hard")
        {
            return await _familyRepository.HardDelete(id);
        }
        return await _familyRepository.SoftDelete(id);
    }
}