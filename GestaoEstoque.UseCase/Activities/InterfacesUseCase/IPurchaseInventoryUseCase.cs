using GestaoEstoque.CoreBusiness;

namespace GestaoEstoque.UseCase.Activities.InterfacesUseCase
{
    public interface IPurchaseInventoryUseCase
    {
        Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string doneBy);
    }
}