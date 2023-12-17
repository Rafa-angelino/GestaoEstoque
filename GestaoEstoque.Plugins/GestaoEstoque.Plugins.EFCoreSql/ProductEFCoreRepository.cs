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
    public class ProductEFCoreRepository : IProductRepository
    {
        private readonly IDbContextFactory<GestaoEstoqueContext> _contextFactory;

        public ProductEFCoreRepository(IDbContextFactory<GestaoEstoqueContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddProductAsync(Product product)
        {
            using var db = _contextFactory.CreateDbContext();
            db.Products.Add(product);
            FlagInventoryUnchanged(product, db);

            await db.SaveChangesAsync();

        }

       

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Products
                .Include(x => x.ProductInventories)
                .ThenInclude(x => x.Inventory)
                .FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Products.Where(
                x => x.ProductName.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            using var db = _contextFactory.CreateDbContext();
            var prod = await db.Products
                .Include(x => x.ProductInventories)
                .FirstOrDefaultAsync(x => x.ProductId == product.ProductId);

            if(prod != null)
            {
                prod.ProductName = product.ProductName;
                prod.Price = product.Price;
                prod.Quantity = product.Quantity;
                prod.ProductInventories = product.ProductInventories;
                FlagInventoryUnchanged(product, db);

                await db.SaveChangesAsync();
            }

        }

        private static void FlagInventoryUnchanged(Product product, GestaoEstoqueContext db)
        {
            if (product?.ProductInventories != null &&
                            product?.ProductInventories.Count() > 0)
            {
                foreach (var prodInv in product.ProductInventories)
                {
                    if (prodInv.Inventory != null)
                    {
                        db.Entry(prodInv.Inventory).State = EntityState.Unchanged;
                    }
                }
            }
        }
    }
}
