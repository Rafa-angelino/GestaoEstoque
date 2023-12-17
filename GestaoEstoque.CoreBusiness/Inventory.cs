using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.CoreBusiness
{
    public class Inventory
    {
        public int InventoryId { get; set; }

        [Required(ErrorMessage="O campo de descrição é obrigatório")]
        public string InventoryName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage="Quantidade deve ser maior ou igual a zero")]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage= "Preço de venda deve ser maior ou igual a zero")]
        public double Price { get; set; }

        public List<ProductInventory> ProductInventories { get; set; } = new();
    }
}
