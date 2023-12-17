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
    public class ViewProductByIdUseCase : IViewProductByIdUseCase
    {
        private readonly IProductRepository _productRepository;

        public ViewProductByIdUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> ExecuteAsync(int productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }
    }
}
