using MediatR;

namespace Stable.Query.Product;

public record ByIdentity(int Identity): IRequest<Result.Product.ByIdentity?>;
