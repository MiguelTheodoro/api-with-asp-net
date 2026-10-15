using MediatR;
using Stable;
using Stable.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Stable.Handler.Product;


public class Delete(Storage storage) : IRequestHandler<Command.Product.Delete, Result.Product.Delete?>
{
    public async Task<Result.Product.Delete?> Handle(Command.Product.Delete request, CancellationToken cancellationToken = default)
    {
        Domain.Product? product = await storage.Products.FirstOrDefaultAsync(product => product.Identity == request.Identity);

        if(product is null) return null;

        storage.Products.Remove(product);

        await storage.SaveChangesAsync(cancellationToken);

        return new ();

    }
}