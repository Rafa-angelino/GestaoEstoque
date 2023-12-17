using GestaoEstoque.CoreBusiness;
using GestaoEstoque.UseCase.Interfaces;
using GestaoEstoque.UseCase.Products.InterfacesUseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.UseCase.Products
{
    
    public class AddProducctUseCase : IAddProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public AddProducctUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task ExecuteAsync(Product product)
        {
            if (product == null) return;

           await _productRepository.AddProductAsync(product);
        }
    }
}
