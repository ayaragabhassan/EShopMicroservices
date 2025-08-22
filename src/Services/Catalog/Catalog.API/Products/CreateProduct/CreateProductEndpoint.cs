namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record CreateProductResponse(Guid Id);


public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
            async (CreateProductRequest request, ISender sender) =>
            {
                // Map the request to the command using Mapster

                var command = request.Adapt<CreateProductCommand>();
                // Send the command using MediatR

                var result = await sender.Send(command);
                // Map the result to the response using Mapster

                var response = result.Adapt<CreateProductResponse>();
                // Return the response with a 201 Created status code

                return Results.Created($"/products/{response.Id}", response);

            })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}

