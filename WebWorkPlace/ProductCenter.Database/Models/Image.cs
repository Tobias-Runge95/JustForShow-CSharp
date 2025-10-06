namespace ProductCenter.Database.Models;

public class Image
{
    [ID]
    public string Id { get; }

    public string AltText { get; set; }
    public string Height { get; set; }
    public string Width { get; set; }
    public string URL { get; set; }
}