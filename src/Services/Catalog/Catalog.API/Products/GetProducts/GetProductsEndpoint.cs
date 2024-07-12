
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{

    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery()) ;
                var response = result.Adapt<GetProductsResult>();

                return Results.Ok(response);
            })
        .WithName("GetProduct")
        .Produces<GetProductsResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product")
        .WithDescription("Get Product");
        }
    }
}
