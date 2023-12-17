using GestaoEstoque.CoreBusiness.Validations;
using System.ComponentModel.DataAnnotations;


namespace GestaoEstoque.CoreBusiness
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "O campo de descrição é obrigatório")]
        public string ProductName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser maior ou igual a zero")]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Preço de venda deve ser maior ou igual a zero")]
        public double Price { get; set; }

        [Product_EnsurePriceIsGreaterThanInventoriesCost]
        public List<ProductInventory> ProductInventories { get; set; } = new();

        public void AddInventory(Inventory inventory)
        {
            if (!ProductInventories.Any(x => x.Inventory != null && x.Inventory.InventoryName.Equals(inventory.InventoryName)))
            {
                this.ProductInventories.Add(new ProductInventory
                {
                    InventoryId = inventory.InventoryId,
                    Inventory = inventory,
                    InventoryQuantity = 1,
                    ProductId = this.ProductId,
                    Product = this
                });
            }
        }
    }
}
