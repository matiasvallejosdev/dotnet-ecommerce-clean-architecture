namespace App.Data;

public class Tag : Base
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Product>? Products { get; set; }
}