namespace App.Data;

public class Tag : Base
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public List<Product>? Products { get; set; }
}