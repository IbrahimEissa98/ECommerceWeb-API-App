using ECommerceApp.API.Endpoints;
using ECommerceApp.API.Extensions;
using ECommerceApp.Application.Extensions;
using ECommerceApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDIForApi();
builder.Services.AddDIForInfrastructure(builder.Configuration);
builder.Services.AddDIForApplication(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapOpenApi().WithDocumentPerVersion();

await app.SeedDatabaseAsync();

if (app.Environment.IsDevelopment())
{
    // Add this
    app.UseSwaggerUI(options =>
    {
        // We reverse the list of API versions so the newest version is rendered first
        foreach (var description in app.DescribeApiVersions().Reverse())
        {
            options.SwaggerEndpoint(
                $"/openapi/{description.GroupName}.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.MapProductEndpoints();

app.Run();
