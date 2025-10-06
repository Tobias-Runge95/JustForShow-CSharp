namespace ProductCenter.Database.Models;

public class Product
{
    [GraphQLDescription("The ID for the product.")]
    [ID]
    public string Id { get; }
}