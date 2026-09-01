using ECommerceApp.API.Common.Extensions;
using ECommerceApp.Application.Extensions;
using ECommerceApp.Infrastructure.Extensions;
using Microsoft.OpenApi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDIForApi();
builder.Services.AddDIForInfrastructure(builder.Configuration);
builder.Services.AddDIForApplication(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        // Enable XML comments if using them for descriptions
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
}

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
    app.UseSwaggerUI(options =>
    {
        // reverse the list of API versions so the newest version is rendered first
        foreach (var description in app.DescribeApiVersions().Reverse())
        {
            options.SwaggerEndpoint(
                $"/openapi/{description.GroupName}.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

//app.MapProductEndpoints();

app.Run();
