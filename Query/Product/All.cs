using MediatR;

namespace Stable.Query.Product;

public record All(): IRequest<List<Result.Product.All>>;
