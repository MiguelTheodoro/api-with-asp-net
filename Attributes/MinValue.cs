using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Stable.Attributes;

public class MinValue<T>: ValidationAttribute where T: INumber<T>
{
    private readonly T min;
    public MinValue(string min)
    {
        this.min = T.Parse(min, null);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

       try
        {
            
            T current = (T) Convert.ChangeType(value, typeof(T));

            if(current < this.min)

                return new ValidationResult(ErrorMessage ?? $"The {validationContext.DisplayName.ToLower()} must be greater than {this.min}");
        }
        catch 
        {
             return new ValidationResult(ErrorMessage ?? $"The provided value could not be compared");
        }

        return ValidationResult.Success;
    }
}

