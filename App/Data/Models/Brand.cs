namespace App.Data;
using System.Collections.Generic;

public class Brand : Base
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Product>? Products { get; set; }
}