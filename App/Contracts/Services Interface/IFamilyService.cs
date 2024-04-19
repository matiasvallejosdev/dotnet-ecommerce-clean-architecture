namespace App.Services;

using App.Contracts;
using App.Data;

public interface IFamilyService
{
    Task<List<Family>> GetAllFamilies();
    Task<Family> GetFamilyById(int id);
    Task<Family> CreateFamily(FamilyPostDto req);
    Task<Family> UpdateFamily(int id, FamilyPatchDto req);
    Task<Family> DeleteFamily(int id, string type);
    Task<Family> UpFamily(int id);
}