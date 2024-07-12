
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductByIdCommand(Guid Id) : ICommand<DeleteProductByIdResult>;
    public record DeleteProductByIdResult(bool isSuccess);
    public class DeleteProductHandler(IDocumentSession session) : ICommandHandler<DeleteProductByIdCommand, DeleteProductByIdResult>
    {
        public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductByIdResult(true);
        }
    }
}
