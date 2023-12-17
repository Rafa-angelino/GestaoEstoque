using System.ComponentModel.DataAnnotations;

namespace GestaoDeEstoque.WebApp.ViewModels
{
    public class PurchaseViewModel
    {
        [Required]
        public string PONumber { get; set; } = string.Empty;

        [Required]
        [Range(minimum:1,maximum:int.MaxValue, ErrorMessage ="Você precisa selecionar um inventário")]
        public int InventoryId { get; set; }
        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantidade precisa ser maior que um.")]
        public int QuantityToPurchase { get; set; }
        public double InventoryPrice { get; set; }
    }
}
