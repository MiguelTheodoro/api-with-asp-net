using MediatR;
using Stable;
namespace Stable.Command.Product;

public record Create(string Name, decimal Price, int Amount): IRequest<Result.Product.Create>;