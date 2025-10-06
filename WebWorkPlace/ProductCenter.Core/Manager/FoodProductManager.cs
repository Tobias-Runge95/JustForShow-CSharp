using ProductCenter.Core.Repositories;
using ProductCenter.Database.Models;

namespace ProductCenter.Core.Manager;

public interface IFoodProductManager
{
    void CreateFoodProduct();
}

public class FoodProductManager : IFoodProductManager
{
    private readonly FoodProductRepository _foodProductRepository;

    public FoodProductManager(FoodProductRepository foodProductRepository)
    {
        _foodProductRepository = foodProductRepository;
    }

    public void CreateFoodProduct()
    {
        // Just for Testing Needs to be changed later

        var newFProduct = new FoodProduct
        {
            Badges = new List<Badge>() { new Badge { Key = "Testing", Value = "Test" } },
            Categories = new List<Category>() { new Category { Name = "Testing" } },
            Image = new Image { AltText = "", Height = "250", Width = "250", URL = "https://Testing" },
            Infos = new Info() { Subtitle = "Das hier ist nur zum Testen da" },
            Media = new List<Media>()
            {
                new Media
                {
                    Image = new Image() { AltText = "", Height = "250", Width = "250", URL = "https://Testing" },
                    MediaType = "Side"
                }
            },
            Name = "Test Product",
            Id = Guid.NewGuid(),
            Options = new List<Option>()
                { new Option() { Name = "Test art", Values = new List<string>() { "test", "Testing", "sachen" } } },
            Review = new ReviewInfo() { ReviewAverage = 4.8f, ReviewCount = 500 },
            Variants = new List<Variant>()
        };
        
        _foodProductRepository.CreateFoodProduct(newFProduct);
    }
}