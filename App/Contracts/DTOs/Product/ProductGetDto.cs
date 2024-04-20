namespace App.Contracts;

public class ProductGetDto
{
    public int Id { get; set; } 
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Stock { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public string BrandName { get; set; } = default!;
    public string FamilyName { get; set; } = default!;
}