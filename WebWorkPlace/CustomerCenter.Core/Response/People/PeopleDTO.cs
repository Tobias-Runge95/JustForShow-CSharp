namespace CustomerCenter.Core.ResponseDTOs.Customer;

public class PeopleDTO
{
    public Guid Id { get; set; }
    public ShortCustomer Customer { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Mobile { get; set; }
    public string Position { get; set; }
}