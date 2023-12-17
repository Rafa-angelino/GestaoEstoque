using GestaoEstoque.CoreBusiness;

namespace GestaoEstoque.UseCase.Reports.InterfacesUseCase
{
    public interface ISearchInventoryTransactionUseCase
    {
        Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType);
    }
}