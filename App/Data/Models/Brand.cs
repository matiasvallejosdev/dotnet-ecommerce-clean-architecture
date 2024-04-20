namespace App.Data;
using System.Collections.Generic;

public class Brand : Base
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public List<Product>? Products { get; set; }
}