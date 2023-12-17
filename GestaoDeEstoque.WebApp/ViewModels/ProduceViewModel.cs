using GestaoDeEstoque.WebApp.ViewModelValidations;
using GestaoEstoque.CoreBusiness;
using System.ComponentModel.DataAnnotations;

namespace GestaoDeEstoque.WebApp.ViewModels
{
    public class ProduceViewModel
    {
        [Required]
        public string ProductionNumber { get; set; } = string.Empty;

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Você precisa selecionar um produto")]
        public int ProductId { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantidade precisa ser maior que um.")]
        [Produce_EnsureEnoughInventoryQuantitiy]
        public int QuantityToProduce { get; set; }

        public Product? Product { get; set; } = null;
    }
}
