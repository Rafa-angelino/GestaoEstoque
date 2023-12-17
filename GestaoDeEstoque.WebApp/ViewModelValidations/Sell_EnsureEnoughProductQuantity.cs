using GestaoDeEstoque.WebApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace GestaoDeEstoque.WebApp.ViewModelValidations
{
    public class Sell_EnsureEnoughProductQuantity : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var sellViewModel = validationContext.ObjectInstance as SellViewModel;
            if (sellViewModel != null)
            {
                if(sellViewModel.Product != null)
                {
                    if(sellViewModel.Product.Quantity < sellViewModel.QuantityToSell) //não possui estoque suficiente
                    {
                        return new ValidationResult($"O produto não possui estoque suficiente. Apenas há {sellViewModel.Product.Quantity}",
                            new[] { validationContext.MemberName });
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
