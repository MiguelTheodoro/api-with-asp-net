using Stable;
namespace Stable.Domain;


public class Product
{

    public int Identity { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }   

    private Product(){}
    public static Product From(Command.Product command)
    {
        return new Product
        {
            Name = command.Name,
            Price = command.Price,
            Amount = command.Amount
        };
    }
    
}

