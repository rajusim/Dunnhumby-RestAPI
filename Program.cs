using Dunnhumby.RestApi;
using Dunnhumby.RestApi.Context;
using Dunnhumby.RestApi.Interface.Repositiries;
using Dunnhumby.RestApi.Interface.Services;
using Dunnhumby.RestApi.Repositories;
using Dunnhumby.RestApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("ProductDb");
Console.WriteLine($"Using connection string: {connectionString}");

builder.Services.AddDbContext<ProductDBContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Replace with your React app's URL
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Use CORS
app.UseCors("AllowReactApp");

var dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
Directory.CreateDirectory(dataFolder);

// Automatically create DB if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductDBContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapProductRoutes();

app.UseHttpsRedirection();

app.Run();


