namespace CustomerCenter.Database.Models;

public class BaseCustomerModelClass
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}