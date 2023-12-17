using GestaoEstoque.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.UseCase.Products.InterfacesUseCase
{
    public interface IViewProductByNameUseCase
    {
        Task<IEnumerable<Product>> ExecuteAsync(string name = "");
    }
}
