namespace ProductCenter.Database.Models;

public class Media
{
    [ID]
    public string Id { get; }

    public string MediaType { get; set; }
    public Image Image { get; set; }
}