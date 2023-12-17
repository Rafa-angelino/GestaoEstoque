using GestaoDeEstoque.WebApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace GestaoDeEstoque.WebApp.ViewModelValidations
{
    public class Produce_EnsureEnoughInventoryQuantitiy : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var produceViewModel = validationContext.ObjectInstance as ProduceViewModel;

            if(produceViewModel != null)
            {
                if(produceViewModel.Product != null && produceViewModel.Product.ProductInventories != null)
                {
                    foreach(var pi in  produceViewModel.Product.ProductInventories)
                    {
                        if (pi.Inventory != null && pi.InventoryQuantity * produceViewModel.QuantityToProduce > pi.Inventory.Quantity)
                        {
                            return new ValidationResult($"O inventário de " +
                                $"({pi.Inventory.InventoryName}) não é o necessário para produzir {produceViewModel.QuantityToProduce} produtos",
                                new[] { validationContext.MemberName });
                        }
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
