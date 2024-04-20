namespace App.Contracts;

public class BrandGetDetailDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}