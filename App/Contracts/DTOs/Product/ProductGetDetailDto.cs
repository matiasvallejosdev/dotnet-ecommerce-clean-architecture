namespace App.Contracts;

public class ProductGetDetailDto : BaseDto
{
    public int Id { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal PriceCost { get; set; } = default!;
    public decimal PriceSale { get; set; } = default!;
    public int Stock { get; set; } = default!;
    public BrandGetDto Brand { get; set; } = default!;
    public FamilyGetDto Family { get; set; } = default!;
    public List<TagGetDto>? Tags { get; set; }
}