using MediatR;
using Stable;
using Stable.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Stable.Handler.Product;


public class ByIdentity(Storage storage) : IRequestHandler<Query.Product.ByIdentity, Result.Product.ByIdentity?>
{
    public async Task<Result.Product.ByIdentity?> Handle(Query.Product.ByIdentity request, CancellationToken cancellationToken = default)
    {
        Domain.Product? product = await storage.Products.FirstOrDefaultAsync(product => product.Identity == request.Identity, cancellationToken);
        
        if(product is null) return null;

        Result.Product.ByIdentity result = new Result.Product.ByIdentity(product.Identity, product.Name, product.Price, product.Amount);

        return result;
    }
}