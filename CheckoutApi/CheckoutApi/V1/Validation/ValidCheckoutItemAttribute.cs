using CheckoutApi.Services;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CheckoutApi.V1.Validation;

public class ValidCheckoutItemAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {     
        if (value is null)
        {
            return new ValidationResult("Id value cannot be null.");
        }

        var stockLookupService = validationContext.GetService(typeof(IStockLookupService)) as IStockLookupService;
        bool isValid = stockLookupService!.IsValidStockItem((string)value);

        if (!isValid)
            return new ValidationResult(string.Format(CultureInfo.CurrentCulture,
            ErrorMessageString, $"Id with value {(string)value}"));

        return ValidationResult.Success!;
    }
}
