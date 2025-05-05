using Dunnhumby.RestApi.Interface.Services;
using Dunnhumby.RestApi.Models;

namespace Dunnhumby.RestApi
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductRoutes(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/products");

            group.MapGet("/", async (IProductService service) =>
                Results.Ok(await service.GetAllAsync()));

            group.MapGet("/{id:int}", async (int id, IProductService service) =>
            {
                var product = await service.GetByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            });

            group.MapPost("/", async (Product product, IProductService service) =>
            {
                var created = await service.AddAsync(product);
                return Results.Created($"/api/products/{created.ProductId}", created);
            });

            group.MapPut("/{id:int}", async (int id, ProductDto product, IProductService service) =>
            {
                var success = await service.UpdateAsync(id, product);
                return success ? Results.NoContent() : Results.NotFound();
            });

            group.MapDelete("/{id:int}", async (int id, IProductService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.NoContent() : Results.NotFound();
            });
            return routes;
        }
    }
}
