using GestaoEstoque.CoreBusiness;
using GestaoEstoque.UseCase.Interfaces;
using GestaoEstoque.UseCase.Products.InterfacesUseCase;

namespace GestaoEstoque.UseCase.Products
{
    public class ViewProductByNameUseCase : IViewProductByNameUseCase
    {
        private readonly IProductRepository _productRepository;
        public ViewProductByNameUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> ExecuteAsync(string name = "")
        {
            return await _productRepository.GetProductByNameAsync(name);
        }
    }
}
