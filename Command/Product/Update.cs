using System.ComponentModel.DataAnnotations;
using MediatR;
using Stable.Attributes;

namespace Stable.Command.Product;

public record Update(

    [Required] int Identity, 
    
    [Required, Length(1, 256)] string Name, 

    [Required, MinValue<decimal>("1")] decimal Price, 

    [Required, MinValue<int>("0")] int Amount

): IRequest<Result.Product.Update?>;