using GestaoEstoque.CoreBusiness;
using GestaoEstoque.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.Plugins.InMemory
{
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private List<ProductTransaction> _productTransaction = new();

        private readonly IProductRepository _productRepository;
        private readonly IInventoryTransactionRepository _inventoryTransactionRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public ProductTransactionRepository(IProductRepository productRepository, 
            IInventoryTransactionRepository inventoryTransactionRepository,
            IInventoryRepository inventoryRepository)
        {
            _productRepository = productRepository;
            _inventoryTransactionRepository = inventoryTransactionRepository;
            _inventoryRepository = inventoryRepository;
        }
        public async Task ProduceAsync(string productionNumber, 
            Product product, int quantity, string doneBy)
        {
            
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
            _productTransaction.Add(new ProductTransaction
            {
                ProductionNumber = productionNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.Quantity,
                ActivityType = ProductTransactionType.ProductProduct,
                QuantityAfter = product.Quantity + quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy
            });

        }

        public Task SellProductAsync(string salesOrderNumber, 
            Product product, int quantity, double unitPrice, string doneBy)
        {
            _productTransaction.Add(new ProductTransaction
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

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(
            string productName, 
            DateTime? dateFrom, 
            DateTime? dateTo, 
            ProductTransactionType? transactionType
            )
        {
            var products = (await _productRepository.GetProductByNameAsync(string.Empty)).ToList();

            var query = from pt in _productTransaction
                        join prod in products on pt.ProductId equals prod.ProductId
                        where
                             (string.IsNullOrWhiteSpace(productName) || prod.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0)
                             &&
                             (!dateFrom.HasValue || pt.TransactionDate >= dateFrom.Value.Date)
                             &&
                             (!dateTo.HasValue || pt.TransactionDate <= dateTo.Value.Date) &&
                             (!transactionType.HasValue || pt.ActivityType == transactionType)
                        select new ProductTransaction
                        {
                            Product = prod,
                            ProductTransactionId = pt.ProductTransactionId,
                            SONumber = pt.SONumber,
                            ProductionNumber = pt.ProductionNumber,
                            ProductId = pt.ProductId,
                            QuantityBefore = pt.QuantityBefore,
                            QuantityAfter = pt.QuantityAfter,
                            ActivityType = pt.ActivityType,
                            TransactionDate = pt.TransactionDate,
                            DoneBy = pt.DoneBy,
                            UnitPrice = pt.UnitPrice,
                        };
            return query;
        }
    }
}
