namespace App.Contracts;

public class ProductPatchDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? PriceCost { get; set; }
    public decimal? PriceSale { get; set; }
    public int? Stock { get; set; }
    public int? FamilyId { get; set; }
    public int? BrandId { get; set; }
    public List<string>? Tags { get; set; }
}