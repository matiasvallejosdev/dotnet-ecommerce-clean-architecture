namespace App.Data;

public class Family : Base
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Product>? Products { get; set; }
}