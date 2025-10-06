using ProductCenter.Core.Repositories;
using ProductCenter.Database.Models;

namespace ProductCenter.Core.Types;

public class Query
{
    public string Hello()
    {
        return "Hello world";
    }

    public List<FoodProduct> BasicFoodProduct(FoodProductRepository foodProductRepository)
    {
        return foodProductRepository.GetAllFoodProducts();
    }
}

public class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field(f => f.Hello())
            .Type<StringType>();
    }
}