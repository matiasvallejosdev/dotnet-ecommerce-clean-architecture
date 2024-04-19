namespace App.Contracts;

using System.Collections.Generic;
using App.Data;

public interface IFamilyRepository
{
    Task<List<Family>> GetActivesFamilies();
    Task<Family> GetFamilyById(int id);
    Task<Family> CreateFamily(Family family);
    Task<Family> UpdateFamily(Family family);
    Task<Family> HardDelete(int id);
    Task<Family> SoftDelete(int id);
    Task<Family> UpFamily(int id);
}