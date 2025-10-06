namespace CustomerCenter.Database.Models;

public class Basket : BaseCustomerModelClass
{
    public Guid CustomerId { get; set; }
    // TODO: Add real Item object from ProductCenter
    public List<object> Items { get; set; }
    public DateOnly? BoughtAt { get; set; }
}