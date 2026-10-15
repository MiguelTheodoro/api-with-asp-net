using MediatR;
using Stable;
using Stable.Repositories;

namespace Stable.Handler.Product;


public class Create(Storage storage) : IRequestHandler<Command.Product.Create, Result.Product.Create>
{
    public async Task<Result.Product.Create> Handle(Command.Product.Create request, CancellationToken cancellationToken = default)
    {
        Domain.Product product = new Domain.Product { Name = request.Name, Price = request.Price, Amount = request.Amount};

        storage.Products.Add(product);

        await storage.SaveChangesAsync(cancellationToken);

        return new Result.Product.Create(product.Identity);
    }
}