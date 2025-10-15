using Azure.Identity;
using CustomerCenter.Core;
using CustomerCenter.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

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
builder.Services.AddDbContext<CustomerCenterDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddControllers();
builder.Services.RegisterCustomerCenterServices();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
