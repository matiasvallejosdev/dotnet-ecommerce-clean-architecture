namespace App.Data;

public class Family : Base
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public List<Product>? Products { get; set; }
}