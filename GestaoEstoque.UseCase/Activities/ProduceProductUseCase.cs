using GestaoEstoque.CoreBusiness;
using GestaoEstoque.UseCase.Activities.InterfacesUseCase;
using GestaoEstoque.UseCase.Interfaces;

namespace GestaoEstoque.UseCase.Activities
{
    public class ProduceProductUseCase : IProduceProductUseCase
    {
        private readonly IProductTransactionRepository _productTransactionRepository;
        private readonly IProductRepository _productRepository;

        public ProduceProductUseCase(IProductTransactionRepository productTransactionRepository, IProductRepository productRepository)
        {
            _productTransactionRepository = productTransactionRepository;
            _productRepository = productRepository;
        }

        public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            //adicionar o record de transação
            await this._productTransactionRepository.ProduceAsync(productionNumber, product, quantity, doneBy);

            //atualizar a quantidade do produto
            product.Quantity += quantity;
            await this._productRepository.UpdateProductAsync(product);
        }
    }
}
