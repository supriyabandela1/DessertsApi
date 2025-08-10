using DessertsApi.Data;
using Microsoft.EntityFrameworkCore;
using DessertsApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://dessertsapp.onrender.com")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                          });
});
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Get the port from environment variable (Render sets this automatically)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

// Tell Kestrel to listen on this port on all network interfaces (0.0.0.0)
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desserts API V1");
});

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");

app.UseAuthorization();
app.MapControllers();

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    // Seed data if table is empty
    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product { id = 1, name = "Waffle with Berries", price = 6.50m, imageUrl = "./assets/images/image-waffle-thumbnail.jpg" },
            new Product { id = 2, name = "Vanilla Bean Crème Brûlée", price = 7.00m, imageUrl = "./assets/images/image-creme-brulee-thumbnail.jpg" },
            new Product { id = 3, name = "Macaron Mix of Five", price = 8.00m, imageUrl = "./assets/images/image-macaron-thumbnail.jpg" },
            new Product { id = 4, name = "Classic Tiramisu",  price = 5.50m, imageUrl = "./assets/images/image-tiramisu-thumbnail.jpg" },
            new Product { id = 5, name = "Pistachio Baklava", price = 4.00m, imageUrl = "./assets/images/image-baklava-thumbnail.jpg" },
            new Product { id = 6, name = "Lemon Meringue Pie", price = 5.00m, imageUrl = "./assets/images/image-meringue-thumbnail.jpg" },
            new Product { id = 7, name = "Red Velvet Cake", price = 4.50m, imageUrl = "./assets/images/image-cake-thumbnail.jpg" },
            new Product { id = 8, name = "Salted Caramel Brownie", price = 4.50m, imageUrl = "./assets/images/image-brownie-thumbnail.jpg" },
            new Product { id = 9, name = "Vanilla Panna Cotta", price = 6.50m, imageUrl = "./assets/images/image-panna-cotta-thumbnail.jpg" }
        );
        db.SaveChanges();
    }
}

app.Run();
