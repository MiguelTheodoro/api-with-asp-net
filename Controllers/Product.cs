using Microsoft.AspNetCore.Mvc;

namespace Stable.Controllers;

using Stable;

[ApiController]
[Route("[controller]")]
public class ProductController: ControllerBase
{

    private List<Domain.Product> list = new List<Domain.Product>();

    public IActionResult Products(Command.Product command)
    {
        Domain.Product product = Domain.Product.From(command);

        this.list.Add(product);

        return CreatedAtAction(nameof(Get), new { identity = product.Identity }, product);
    }

    [HttpGet("{identity}")]
    public IActionResult Get(Guid identity)
    {

        Domain.Product? product = this.list.FirstOrDefault(product => product.Identity == identity);

        if(product is null) return NotFound();

        Result.Product response = Result.Product.From(product);

        return Ok(response);

    }
}