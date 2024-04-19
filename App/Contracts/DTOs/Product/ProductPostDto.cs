namespace App.Contracts;

public class ProductPostDto
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = "";
    public decimal PriceCost { get; set; }
    public decimal PriceSale { get; set; }
    public int Stock { get; set; }
    public required int FamilyId { get; set; }
    public required int BrandId { get; set; }
    public List<string>? Tags { get; set; }
}