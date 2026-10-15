using MediatR;

namespace Stable.Command.Product;

public record Delete(int Identity): IRequest<Result.Product.Delete?>;