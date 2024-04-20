namespace App.Contracts;

public class FamilyGetDetailDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}