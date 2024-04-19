namespace App.Contracts;

public class ProductGetDto
{
    public required int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required int Stock { get; set; }
    public required decimal Price { get; set; }
    public required string BrandName { get; set; }
    public required string FamilyName { get; set; }
}