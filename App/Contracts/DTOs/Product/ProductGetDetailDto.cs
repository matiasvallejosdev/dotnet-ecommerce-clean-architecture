namespace App.Contracts;

public class ProductGetDetailDto : BaseDto
{
    public required int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal PriceCost { get; set; }
    public required decimal PriceSale { get; set; }
    public required int Stock { get; set; }
    public required BrandGetDto Brand { get; set; }
    public required FamilyGetDto Family { get; set; }
    public List<TagGetDto>? Tags { get; set; }
}