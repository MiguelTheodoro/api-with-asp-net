using MediatR;
using Stable;
using Stable.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Stable.Handler.Product;


public class Update(Storage storage) : IRequestHandler<Command.Product.Update, Result.Product.Update?>
{
    public async Task<Result.Product.Update?> Handle(Command.Product.Update request, CancellationToken cancellationToken)
    {
    
        Domain.Product? product = await storage.Products.FirstOrDefaultAsync(product => product.Identity == request.Identity);

        if(product is null) return null;

        storage.Entry(product).CurrentValues.SetValues(request);

        await storage.SaveChangesAsync(cancellationToken);

        return new Result.Product.Update(product.Name, product.Price, product.Amount);

    }
}