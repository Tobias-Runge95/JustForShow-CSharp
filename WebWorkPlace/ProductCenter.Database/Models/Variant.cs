namespace ProductCenter.Database.Models;

public class Variant
{
    [ID]
    public string Id { get; }

    public string Title { get; set; }
    public string SKU { get; set; }
    public Image Image { get; set; }
    public List<Media> Media { get; set; }
    public List<Badge> Badges { get; set; }
    public string NutritionValues { get; set; }
    public string Ingredients { get; set; }
    public List<string> Portions { get; set; }
    public Price Price { get; set; }
    public int QuantityAvailable { get; set; }
    public UnitPrice UnitPrice { get; set; }
    public UnitPriceMeasurement UnitPriceMeasurement { get; set; }
}