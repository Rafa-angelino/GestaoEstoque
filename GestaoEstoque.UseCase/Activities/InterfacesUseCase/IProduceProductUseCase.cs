using GestaoEstoque.CoreBusiness;

namespace GestaoEstoque.UseCase.Activities.InterfacesUseCase
{
    public interface IProduceProductUseCase
    {
        Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy);
    }
}