using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Stable.Middleware;


public class Qualifier<Request, Response> : IPipelineBehavior<Request, Response> where Request : IRequest<Response>
{
    public async Task<Response> Handle(Request request, RequestHandlerDelegate<Response> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext(request, serviceProvider: null, items: null);
        
        var errors  = new List<ValidationResult>();

        if(!Validator.TryValidateObject(request, context, errors, validateAllProperties: true))

            throw new ValidationException("Invalid datas: " + string.Join(", ", errors.Select(error => error.ErrorMessage)));

        return await next();
    }
}