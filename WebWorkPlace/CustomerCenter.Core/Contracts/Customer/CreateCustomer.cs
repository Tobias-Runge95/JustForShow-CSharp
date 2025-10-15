namespace CustomerCenter.Core.Contracts.Customer;

public class CreateCustomer
{
    public string Name { get; set; }
    public string Currency { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }

    public Database.Models.Customer MapToCustomer()
    {
        return new Database.Models.Customer
        {
            Country = Country,
            Currency = Currency,
            Street = Street,
            HouseNumber = HouseNumber,
            ZipCode = ZipCode
        };
    }
}