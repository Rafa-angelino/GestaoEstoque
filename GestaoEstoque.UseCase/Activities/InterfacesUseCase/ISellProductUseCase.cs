using GestaoEstoque.CoreBusiness;

namespace GestaoEstoque.UseCase.Activities.InterfacesUseCase
{
    public interface ISellProductUseCase
    {
        Task ExecuteAsync(string salesOrderNumber, Product product, int quantity,double unitPrice, string doneBy);
    }
}