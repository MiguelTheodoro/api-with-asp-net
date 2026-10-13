using Microsoft.AspNetCore.Mvc;

namespace Stable.Controllers;
using Stable.Repositories;
using Stable;
using Stable.Domain;

[ApiController]
[Route("[controller]")]
public class ProductController(Storage storage): ControllerBase
{
    public async Task<IActionResult> Products(Command.Product command)
    {

        Domain.Product product = Domain.Product.From(command);

        storage.Products.Add(product);

        await storage.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { identity = product.Identity }, product);

    }

    [HttpGet("{identity}")]
    public async Task<IActionResult> Get(int identity)
    {

        Domain.Product? product = await storage.Products.FindAsync(identity);
        
        if(product is null) return NotFound();

        Result.Product response = Result.Product.From(product);

        return Ok(response);

    }
}