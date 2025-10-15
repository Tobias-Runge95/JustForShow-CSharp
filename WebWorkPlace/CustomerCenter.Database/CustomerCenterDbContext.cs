using CustomerCenter.Database.Models;
using Microsoft.EntityFrameworkCore;


namespace CustomerCenter.Database;

public class CustomerCenterDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<People> Peoples { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ProductionOrder> ProductionOrders { get; set; }

    public CustomerCenterDbContext(DbContextOptions<CustomerCenterDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("crm");
        
        var customer = builder.Entity<Customer>();
        customer.HasKey(x => x.Id);
        
        var order = builder.Entity<Order>();
        order.HasKey(x => x.Id);
        order.HasOne(x => x.Customer).WithMany(x => x.Orders).HasForeignKey(x => x.CustomerId);
        
        var orderItem = builder.Entity<OrderItem>();
        orderItem.HasKey(x => x.Id);
        orderItem.HasOne(x => x.Order).WithMany(x => x.OrderItems).HasForeignKey(x => x.OrderId);
        
        var productionOrder = builder.Entity<ProductionOrder>();
        productionOrder.HasKey(x => x.Id);
        productionOrder.HasOne(x => x.OrderItem).WithOne(x => x.ProductionOrder).HasForeignKey<ProductionOrder>(x => x.OrderItemId);
    }
}