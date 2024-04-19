namespace App.Contracts;

public class BrandGetDetailDto : BaseDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}