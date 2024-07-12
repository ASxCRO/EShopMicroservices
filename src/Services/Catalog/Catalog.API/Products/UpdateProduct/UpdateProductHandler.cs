
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price, Guid Id) 
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductQueryHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            product.Name = command.Name;
            product.Price = command.Price;
            product.Description = command.Description;
            product.Category = command.Category;
            product.ImageFile = command.ImageFile;

             session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }  
    }
}
