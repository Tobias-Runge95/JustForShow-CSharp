using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ProductCenter.Core.Manager;
using ProductCenter.Core.Repositories;
using ProductCenter.Core.Types;
using ProductCenter.Database;
using ProductCenter.Database.Models;

namespace ProductCenter.Core;

public static class Startup
{
    public static WebApplicationBuilder RegisterProductCenterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddGraphQLServer()
            .AddQueryType<QueryType>()
            .AddType<FoodProduct>()
            .AddType<Product>()
            .RegisterService<FoodProductManager>()
            .RegisterService<ProductRepository>();
        builder.Services
            .AddScoped<FoodProductRepository>()
            .AddScoped<ProductRepository>()
            .AddScoped<IFoodProductManager, FoodProductManager>();
        return builder;
        //.AddScoped<ProductCenterDBContext>();
    }

    public static WebApplication AddApplicationParts(this WebApplication app)
    {
        app.MapGraphQL();
        return app;
    }
}