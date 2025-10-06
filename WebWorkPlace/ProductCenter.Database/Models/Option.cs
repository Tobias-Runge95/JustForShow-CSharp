namespace ProductCenter.Database.Models;

public class Option
{
    [ID]
    public string Id { get; }
    public string Name { get; set; }
    public List<string> Values { get; set; }
}