using Stable;
namespace Stable.Domain;
public class Product
{
    public Guid Identity { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }

    public static Product From(Command.Product product)
    {
        return new Product
        {
            Identity = Guid.NewGuid(),
            Name = product.Name,
            Price = product.Price,
            Amount = product.Amount
        };
    }
    
}