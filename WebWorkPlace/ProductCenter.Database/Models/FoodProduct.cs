namespace ProductCenter.Database.Models;

public class FoodProduct
{
    [GraphQLDescription("The ID for the foodProduct.")]
    [ID]
    public Guid Id { get; set; }

    public string Name { get; set; }
    public List<Variant> Variants { get; set; }
    public Image Image { get; set; }
    public List<Badge> Badges { get; set; }
    public List<Media> Media { get; set; }
    public List<Option> Options { get; set; }
    public ReviewInfo Review { get; set; }
    public Info Infos { get; set; }
    public List<Category> Categories { get; set; }
}