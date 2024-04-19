namespace App.Contracts;

public class ProductFilters
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public int? StockGreaterThan { get; set; }
    public int? StockLessThan { get; set; }
    public int? IdFamily { get; set; }
    public int? IdBrand { get; set; }
    public bool? IsDown { get; set; }
}