using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Stable.Attributes;

namespace Stable.Command.Product;

public record Update: IRequest<Result.Product.Update?> {

    [Required][Length(1, 256)] public string Name { get; init; } = string.Empty;

    [Required][MinValue<decimal>("1")] public decimal Price { get; init; } 

    [Required][MinValue<int>("0")] public int Amount { get; init; }

    [Required] public int Identity { get; init; }

};