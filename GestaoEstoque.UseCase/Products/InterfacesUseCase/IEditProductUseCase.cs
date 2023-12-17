using GestaoEstoque.CoreBusiness;

namespace GestaoEstoque.UseCase.Products.InterfacesUseCase
{
    public interface IEditProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}