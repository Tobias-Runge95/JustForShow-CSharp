namespace CustomerCenter.Core.ResponseDTOs.Customer;

public class ShortCustomer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
}