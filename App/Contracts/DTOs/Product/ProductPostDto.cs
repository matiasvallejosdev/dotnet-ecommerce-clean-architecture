namespace App.Contracts;

public class ProductPostDto
{
    public string Code { get; set; } = default!;
    public string Name { get; set; }= default!;
    public string Description { get; set; } = "";
    public decimal PriceCost { get; set; }
    public decimal PriceSale { get; set; }
    public int Stock { get; set; }
    public int FamilyId { get; set; } = default!;
    public int BrandId { get; set; } = default!;
    public List<string>? Tags { get; set; }
}