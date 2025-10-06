using System.Net.Mime;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using IdentityCenter.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary;
using WebWorkPlace.Database;
using WebWorkPlace.Database.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
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
await CryptographyClientExtension.RegisterCryptographyClient(builder);
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("admin", policy => policy.RequireRole("admin"));
    option.AddPolicy("user", policy => policy.RequireRole("user"));
});
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        var keyClient = serviceProvider.GetRequiredService<KeyClient>();
        var publicKey = keyClient.GetKey(builder.Configuration["KeyVault:KeyName"]).Value;
        var rsaKey = new RsaSecurityKey(publicKey.Key.ToRSA());
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = rsaKey,
            ValidIssuer = "IC"
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
