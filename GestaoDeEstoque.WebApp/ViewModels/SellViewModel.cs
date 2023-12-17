using GestaoDeEstoque.WebApp.ViewModelValidations;
using GestaoEstoque.CoreBusiness;
using System.ComponentModel.DataAnnotations;

namespace GestaoDeEstoque.WebApp.ViewModels
{
    public class SellViewModel
    {
        [Required(ErrorMessage ="Obrigatório preenchimento.")]
        public string SalesOrderNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Obrigatório preenchimento.")]
        public int ProductId { get; set; }

        [Required]
        [Sell_EnsureEnoughProductQuantity]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantidade precisa ser maior que um.")]
        public int QuantityToSell { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Preço precisa ser maior ou igual a um.")]
        public double UnitPrice { get; set; }
        public Product? Product { get; set; } = null;
    }
}
