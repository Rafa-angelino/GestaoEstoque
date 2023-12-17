using GestaoEstoque.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.UseCase.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductByNameAsync(string name);
        Task AddProductAsync(Product product);
        Task<Product?> GetProductByIdAsync(int productId);
        Task UpdateProductAsync(Product product);
    }
}
