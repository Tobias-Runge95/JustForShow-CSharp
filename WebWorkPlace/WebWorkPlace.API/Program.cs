using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using CustomerCenter.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using ProductCenter.Core;
using ProductCenter.Database;
using WebWorkPlace.API.Extensions;
using WebWorkPlace.Core;
using WebWorkPlace.Core.Identity;
using WebWorkPlace.Database;
using WebWorkPlace.Database.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAzureClients(azureBuilder =>
{
    azureBuilder.AddKeyClient(new Uri($"https://authserver.vault.azure.net/"));
    // azureBuilder.AddCryptographyClient(new Uri($"https://authserver.vault.azure.net/"));
    azureBuilder.UseCredential(new ClientSecretCredential(
        tenantId: builder.Configuration["KeyVault:TenantId"],
        clientId: builder.Configuration["KeyVault:ClientId"], 
        clientSecret: builder.Configuration["KeyVault:Secret"]
    ));
});
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("admin", policy => policy.RequireRole("admin"));
    option.AddPolicy("user", policy => policy.RequireRole("user"));
});


builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<UserManager<User>>()
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddRoleStore<RoleStore<Role, ApplicationDbContext, Guid>>()
    .AddUserStore<UserStore<User, IdentityRole<Guid>, ApplicationDbContext, Guid>>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddScoped<ProductCenterDBContext>(options =>
{
    var productCenterDBContext =
        new ProductCenterDBContext(); //builder.Configuration.GetConnectionString("ProductCenterConnectionString")
    return productCenterDBContext;
});
builder.Services.AddControllers();
builder.Services.RegisterServices();
builder.Services.RegisterCustomerCenterServices();
builder.RegisterProductCenterServices();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("https://studio.apollographql.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    app.UseCors();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.AddApplicationParts();

app.Use((context, next) =>
{
    var user = context.User;
    return next();
});

app.Run();
