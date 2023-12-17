using GestaoEstoque.CoreBusiness;

namespace GestaoEstoque.UseCase.Reports.InterfacesUseCase
{
    public interface ISearchProductTransactionUseCase
    {
        Task<IEnumerable<ProductTransaction>> ExecuteAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType);
    }
}