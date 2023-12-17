using GestaoEstoque.CoreBusiness;
using GestaoEstoque.UseCase.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.Plugins.EFCoreSql
{
    public class InventoryEFCoreRepository : IInventoryRepository
    {
        private readonly IDbContextFactory<GestaoEstoqueContext> _contextFactory;

        public InventoryEFCoreRepository(IDbContextFactory<GestaoEstoqueContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task AddInventoryAsync(Inventory inventory)
        {
            using var db = _contextFactory.CreateDbContext();
            db.Inventories.Add(inventory);
            await db.SaveChangesAsync();
            

            
        }

        public async Task EditInventoryAsync(Inventory inventory)
        {
            using var db = _contextFactory.CreateDbContext();
            var inv = await db.Inventories.FindAsync(inventory.InventoryId);
            if(inv != null)
            {
                inv.InventoryName = inventory.InventoryName;
                inv.Price = inventory.Price;
                inv.Quantity = inventory.Quantity;

                await db.SaveChangesAsync();
            }
        }

        public Task<bool> ExistsAsync(Inventory inventory)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Inventories.Where(
                x => x.InventoryName.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            var inv = await db.Inventories.FindAsync(id);
            if (inv != null) return inv;

            return new Inventory();
        }
    }
}
