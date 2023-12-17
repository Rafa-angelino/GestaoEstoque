using GestaoEstoque.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.UseCase.Products.InterfacesUseCase
{
    public interface IAddProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}
