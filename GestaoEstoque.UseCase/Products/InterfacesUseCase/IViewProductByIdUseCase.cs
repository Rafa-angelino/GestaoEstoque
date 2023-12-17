using GestaoEstoque.CoreBusiness;

namespace GestaoEstoque.UseCase.Products.InterfacesUseCase
{
    public interface IViewProductByIdUseCase
    {
        Task<Product?> ExecuteAsync(int productId);
    }
}