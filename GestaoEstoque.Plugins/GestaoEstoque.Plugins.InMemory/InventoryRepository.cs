using GestaoEstoque.CoreBusiness;
using GestaoEstoque.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.Plugins.InMemory
{
    public class InventoryRepository : IInventoryRepository
    {
        private List<Inventory> _inventories;

        public InventoryRepository()
        {
            _inventories = new List<Inventory>()
            {
                new Inventory() { InventoryId = 1, InventoryName =  "Colchão1", Quantity = 20, Price = 200 },
                new Inventory() { InventoryId = 2, InventoryName =  "Colchão2", Quantity = 30, Price = 300 },
                new Inventory() { InventoryId = 3, InventoryName =  "Colchão3", Quantity = 10, Price = 200 },
                new Inventory() { InventoryId = 4, InventoryName =  "Colchão4", Quantity = 40, Price = 40 },

            };
        }

        public Task AddInventoryAsync(Inventory inventory)
        {
            var maxId = _inventories.Max(x => x.InventoryId);
            inventory.InventoryId = maxId + 1;
            
            _inventories.Add(inventory);


            return Task.CompletedTask;
        }

        public Task EditInventoryAsync(Inventory inventory)
        {
            if (!_inventories.Any(x => x.InventoryId == inventory.InventoryId && x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
                return Task.CompletedTask;


            var inv = _inventories.FirstOrDefault(x => x.InventoryId == inventory.InventoryId);
            if(inv != null)
            {
                inv.InventoryName = inventory.InventoryName;
                inv.Quantity = inventory.Quantity;
                inv.Price = inventory.Price;
            }
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Inventory inventory)
        {
            return await Task.FromResult(_inventories.Any(x => x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)));
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_inventories);

            return _inventories.Where(x => x.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Inventory> GetInventoryByIdAsync(int id)
        {
            var inv =  await Task.FromResult(_inventories.First(x => x.InventoryId == id));
            var newInv = new Inventory
            { 
                InventoryId = inv.InventoryId, 
                InventoryName = inv.InventoryName, 
                Price = inv.Price, 
                Quantity = inv.Quantity 
            };

            return await Task.FromResult(newInv);   
        }
    }
}
