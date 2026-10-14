using Microsoft.AspNetCore.Mvc;

namespace Stable.Controllers;
using Stable.Repositories;
using Stable;
using Stable.Domain;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ProductController(Storage storage): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CraeteProduct(Command.Product command, CancellationToken cancellationToken = default)
    {
        Domain.Product product = Domain.Product.From(command);

        storage.Products.Add(product);

        await storage.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetProductByIdentity), new { identity = product.Identity }, product);

    }

    [HttpPut("{identity}")]
    public async Task<IActionResult> UpdateProduct(int identity, Command.Product command,  CancellationToken cancellationToken = default)
    {
        Domain.Product? product = await storage.Products.FirstOrDefaultAsync(product => product.Identity == identity);
        //Domain.Product? product = await storage.Products.SingleOrDefaultAsync(product => product.Identity == identity);
        

        if(product is null) return NotFound();

        /*product.Name = command.Name;
        product.Price = command.Price;

        storage.Products.Update(product);*/

        storage.Entry(product).CurrentValues.SetValues(command);

        await storage.SaveChangesAsync(cancellationToken);

        return NoContent();
    
        
    }

    [HttpDelete("{identity}")]
    public async Task<IActionResult> DeleteProduct(int identity, Command.Product command,  CancellationToken cancellationToken = default)
    {
        Domain.Product? product = await storage.Products.FirstOrDefaultAsync(product => product.Identity == identity);

        if(product is null) return NotFound();

        storage.Products.Remove(product);

        await storage.SaveChangesAsync(cancellationToken);

        return Ok();
    
        
    }

    [HttpGet("{identity}")]
    public async Task<IActionResult> GetProductByIdentity(int identity, CancellationToken cancellationToken = default)
    {

        Domain.Product? product = await storage.Products.FirstOrDefaultAsync(product => product.Identity == identity, cancellationToken);
        
        if(product is null) return NotFound();

        Result.Product response = Result.Product.From(product);

        return Ok(response);

    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken = default)
    {
        List<Result.Product> products = await storage.Products.Select(product => Result.Product.From(product)).ToListAsync(cancellationToken);

        return Ok(products);
    }


}