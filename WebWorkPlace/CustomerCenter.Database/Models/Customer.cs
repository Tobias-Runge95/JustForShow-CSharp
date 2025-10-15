namespace CustomerCenter.Database.Models;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Currency { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Order> Orders { get; set; }
    public List<People> Contacts { get; set; }
}