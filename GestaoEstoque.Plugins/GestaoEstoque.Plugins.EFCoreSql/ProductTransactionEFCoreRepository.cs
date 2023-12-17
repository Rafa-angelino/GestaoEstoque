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
    public class ProductTransactionEFCoreRepository : IProductTransactionRepository
    {

        private readonly IProductRepository _productRepository;
        private readonly IInventoryTransactionRepository _inventoryTransactionRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IDbContextFactory<GestaoEstoqueContext> _contextFactory;

        public ProductTransactionEFCoreRepository(
            IProductRepository productRepository, 
            IInventoryTransactionRepository inventoryTransactionRepository,
            IInventoryRepository inventoryRepository,
            IDbContextFactory<GestaoEstoqueContext> contextFactory)
        {
            _productRepository = productRepository;
            _inventoryTransactionRepository = inventoryTransactionRepository;
            _inventoryRepository = inventoryRepository;
            _contextFactory = contextFactory;
            ;
        }
        public async Task ProduceAsync(string productionNumber, 
            Product product, int quantity, string doneBy)
        {
            using var db = _contextFactory.CreateDbContext();

            var prod = await _productRepository.GetProductByIdAsync(product.ProductId);
            if(prod != null)
            {
                foreach(var pi in prod.ProductInventories)
                {
                    if(pi.Inventory != null)
                    {
                        //adicionar transação de inventário
                        await _inventoryTransactionRepository.ProduceAsync(productionNumber, 
                            pi.Inventory, pi.InventoryQuantity * quantity, doneBy, -1);


                        //diminuir o inventories
                        var inv = await  _inventoryRepository.GetInventoryByIdAsync(pi.InventoryId);
                        inv.Quantity -= pi.InventoryQuantity * quantity;
                        await _inventoryRepository.EditInventoryAsync(inv);
                    }
                    
                }
            }

            //adicionar a transação de produto
            db.ProductTransactions.Add(new ProductTransaction
            {
                ProductionNumber = productionNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.Quantity,
                ActivityType = ProductTransactionType.ProductProduct,
                QuantityAfter = product.Quantity + quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy
            });

            await db.SaveChangesAsync();

        }

        public async Task SellProductAsync(string salesOrderNumber, 
            Product product, int quantity, double unitPrice, string doneBy)
        {
            using var db = _contextFactory.CreateDbContext();
            db.ProductTransactions.Add(new ProductTransaction
            {
                ActivityType = ProductTransactionType.SellProduct,
                SONumber = salesOrderNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.Quantity,
                QuantityAfter = product.Quantity - quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = unitPrice
            });

            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(
            string productName, 
            DateTime? dateFrom, 
            DateTime? dateTo, 
            ProductTransactionType? transactionType
            )
        {
            using var db = _contextFactory.CreateDbContext();


            var query = from pt in db.ProductTransactions
                        join prod in db.Products on pt.ProductId equals prod.ProductId
                        where
                             (string.IsNullOrWhiteSpace(productName) || prod.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0)
                             &&
                             (!dateFrom.HasValue || pt.TransactionDate >= dateFrom.Value.Date)
                             &&
                             (!dateTo.HasValue || pt.TransactionDate <= dateTo.Value.Date) &&
                             (!transactionType.HasValue || pt.ActivityType == transactionType)
                        select pt;
            return await query.Include(x => x.Product).ToListAsync();
        }
    }
}
