using GestaoEstoque.CoreBusiness;

namespace GestaoEstoque.UseCase.Interfaces
{
    public interface IProductTransactionRepository
    {
        Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType);
        Task ProduceAsync(string productionNumber, Product product, int quantity,  string doneBy);
        Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double unitPrice, string doneBy);
    }
}