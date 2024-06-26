namespace App.Data;

using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class Product : Base
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal PriceCost { get; set; } = 0;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal PriceSale { get; set; } = 0;

    public int Stock { get; set; } = 0;

    public int FamilyId { get; set; }
    public Family? Family { get; set; }
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }

    public List<Tag>? Tags { get; set; }
}