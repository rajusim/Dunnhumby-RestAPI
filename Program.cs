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

var app = builder.Build();

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


