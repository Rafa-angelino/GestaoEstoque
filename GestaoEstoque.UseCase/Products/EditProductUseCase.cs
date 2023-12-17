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
    public class EditProductUseCase : IEditProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public EditProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ExecuteAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }
    }
}
