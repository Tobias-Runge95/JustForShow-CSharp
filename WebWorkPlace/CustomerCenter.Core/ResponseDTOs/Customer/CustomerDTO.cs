namespace CustomerCenter.Core.ResponseDTOs.Customer;

public class CustomerDTO
{
    public Guid CustomerId { get; set; }
    public string Currency { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
}