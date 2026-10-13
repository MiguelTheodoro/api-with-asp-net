namespace Stable.Result;

using Stable;
public class Product
{
    public int Identity { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }

    public static Product From(Domain.Product product)
    {
        return new Product
        {
            Identity = product.Identity,
            Name = product.Name,
            Price = product.Price,
            Amount = product.Amount
        };
    }
    
}