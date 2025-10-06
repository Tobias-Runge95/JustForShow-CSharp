namespace ProductCenter.Database.Models;

public class Category
{
    [ID]
    public string Id { get; }

    public string Name { get; set; }
}