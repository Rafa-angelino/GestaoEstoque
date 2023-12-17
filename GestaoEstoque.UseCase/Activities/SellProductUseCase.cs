using GestaoEstoque.CoreBusiness;
using GestaoEstoque.UseCase.Activities.InterfacesUseCase;
using GestaoEstoque.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.UseCase.Activities
{
    public class SellProductUseCase : ISellProductUseCase
    {
        private readonly IProductTransactionRepository productTransactionRepository;
        private readonly IProductRepository productRepository;

        public SellProductUseCase(IProductTransactionRepository productTransactionRepository, 
            IProductRepository productRepository)
        {
            this.productTransactionRepository = productTransactionRepository;
            this.productRepository = productRepository;
        }
        public async Task ExecuteAsync(string salesOrderNumber, 
            Product product, int quantity,
            double unitPrice, string doneBy)
        {
            await this.productTransactionRepository.SellProductAsync(salesOrderNumber, 
                product, quantity,unitPrice, doneBy);

            product.Quantity -= quantity;
            await this.productRepository.UpdateProductAsync(product);
        }
    }
}
