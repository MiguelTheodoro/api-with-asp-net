using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Stable.Command.Product;

public record Delete([Required] int Identity): IRequest<Result.Product.Delete?>;