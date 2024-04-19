namespace App.Contracts;

public class FamilyGetDetailDto : BaseDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}