using Microsoft.AspNetCore.Mvc;

namespace Stable.Controllers;

using System.ComponentModel.DataAnnotations;
using MediatR;

[ApiController]
[Route("[controller]")]
public class ProductController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CraeteProduct(Command.Product.Create command)
    {

        try
        {
            Result.Product.Create product = await mediator.Send(command);

            return CreatedAtAction(nameof(GetProductByIdentity), new { identity = product.Identity }, product);
            
        }catch(ValidationException error)
        {
            return BadRequest(new { Error = error.Message});
        }


    }

    [HttpPut("{identity}")]
    public async Task<IActionResult> UpdateProduct(int identity, Command.Product.Update command)
    {

        try
        {
            Command.Product.Update request = command with  { Identity = identity };

            Result.Product.Update? result = await mediator.Send(request);
            
            if(result is null) return NotFound();

            return NoContent();

        }catch(ValidationException error)
        {
            return BadRequest(new { Error = error.Message.Split(", ")});
        }    

    }

    [HttpDelete("{identity}")]
    public async Task<IActionResult> DeleteProduct(int identity)
    {

        try
        {
            Command.Product.Delete request = new Command.Product.Delete(identity);

            Result.Product.Delete? result = await mediator.Send(request);
            
            if(result is null) return NotFound();

            return Ok();

        }catch(ValidationException error)
        {
            return BadRequest(new { Error = error.Message});
        }   

    }

    [HttpGet("{identity}")]
    public async Task<IActionResult> GetProductByIdentity(int identity)
    {
        try
        {

            Query.Product.ByIdentity request = new Query.Product.ByIdentity(identity);

            Result.Product.ByIdentity? result = await mediator.Send(request);
            
            if(result is null) return NotFound();

            return Ok(result);

        }catch(ValidationException error)
        {
            return BadRequest(new { Error = error.Message});
        }

    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(){    

        try
        {
            Query.Product.All request = new Query.Product.All();
        
            List<Result.Product.All> result = await mediator.Send(request);

            return Ok(result);

        }catch(ValidationException error)
        {
            return BadRequest(new { Error = error.Message});
        }

    }


}