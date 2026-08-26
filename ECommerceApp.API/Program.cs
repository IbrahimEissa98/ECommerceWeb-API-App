using ECommerceApp.API.Extensions;
using ECommerceApp.Application.Extensions;
using ECommerceApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDIForApi();
builder.Services.AddDIForInfrastructure(builder.Configuration);
builder.Services.AddDIForApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.SeedDatabaseAsync();

app.Run();
