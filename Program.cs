using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ECommerceDb"));
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/products", async (AppDbContext db) => await db.Products.ToListAsync());

app.MapGet("/categories", async (AppDbContext db) =>
    await db.Categories
    .Select(c => new { c.Id, c.Name })
    .ToListAsync());

app.MapGet("/categories/{id}/products", async (AppDbContext db, int id) =>
{
    return await db.Products.Where(p => p.CategoryId == id)
    .ToListAsync();
});

app.MapPost("/products", async (Product product, AppDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
});

app.MapPost("/categories", async (Category category, AppDbContext db) =>
{
    db.Categories.Add(category);
    await db.SaveChangesAsync();
    return Results.Created($"/categories/{category.Id}", category);
});

app.UseHttpsRedirection();

app.Run();
