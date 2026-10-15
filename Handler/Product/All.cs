using MediatR;
using Stable.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Stable.Handler.Product;


public class All(Storage storage) : IRequestHandler<Query.Product.All, List<Result.Product.All>>
{
    public async Task<List<Result.Product.All>> Handle(Query.Product.All request, CancellationToken cancellationToken = default)
    {

        List<Result.Product.All> products = await storage.Products.Select(product => new Result.Product.All(product.Identity, product.Name, product.Price, product.Amount)).ToListAsync(cancellationToken);

        return products;

    }
}