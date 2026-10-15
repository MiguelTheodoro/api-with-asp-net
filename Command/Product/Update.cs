using MediatR;

namespace Stable.Command.Product;

public record Update(int Identity, string Name, decimal Price, int Amount): IRequest<Result.Product.Update?>;