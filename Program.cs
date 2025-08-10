using DessertsApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://dessertsapp.onrender.com/")
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
            new Product { Id = 1, Name = "Waffle with Berries", Category = "Waffle", Price = 6.50m, ThumbnailUrl = "./assets/images/image-waffle-thumbnail.jpg" },
            new Product { Id = 2, Name = "Vanilla Bean Crème Brûlée", Category = "Crème Brûlée", Price = 7.00m, ThumbnailUrl = "./assets/images/image-creme-brulee-thumbnail.jpg" },
            new Product { Id = 3, Name = "Macaron Mix of Five", Category = "Macaron", Price = 8.00m, ThumbnailUrl = "./assets/images/image-macaron-thumbnail.jpg" },
            new Product { Id = 4, Name = "Classic Tiramisu", Category = "Tiramisu", Price = 5.50m, ThumbnailUrl = "./assets/images/image-tiramisu-thumbnail.jpg" },
            new Product { Id = 5, Name = "Pistachio Baklava", Category = "Baklava", Price = 4.00m, ThumbnailUrl = "./assets/images/image-baklava-thumbnail.jpg" },
            new Product { Id = 6, Name = "Lemon Meringue Pie", Category = "Pie", Price = 5.00m, ThumbnailUrl = "./assets/images/image-meringue-thumbnail.jpg" },
            new Product { Id = 7, Name = "Red Velvet Cake", Category = "Cake", Price = 4.50m, ThumbnailUrl = "./assets/images/image-cake-thumbnail.jpg" },
            new Product { Id = 8, Name = "Salted Caramel Brownie", Category = "Brownie", Price = 4.50m, ThumbnailUrl = "./assets/images/image-brownie-thumbnail.jpg" },
            new Product { Id = 9, Name = "Vanilla Panna Cotta", Category = "Panna Cotta", Price = 6.50m, ThumbnailUrl = "./assets/images/image-panna-cotta-thumbnail.jpg" }
        );
        db.SaveChanges();
    }
}

app.Run();
