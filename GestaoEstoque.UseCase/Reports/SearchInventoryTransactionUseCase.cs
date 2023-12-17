
using GestaoEstoque.CoreBusiness;
using GestaoEstoque.UseCase.Interfaces;
using GestaoEstoque.UseCase.Reports.InterfacesUseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.UseCase.Reports
{
    public class SearchInventoryTransactionUseCase : ISearchInventoryTransactionUseCase
    {
        private readonly IInventoryTransactionRepository inventoryTransactionRepository;

        public SearchInventoryTransactionUseCase(IInventoryTransactionRepository inventoryTransactionRepository)
        {
            this.inventoryTransactionRepository = inventoryTransactionRepository;
        }
        public async Task<IEnumerable<InventoryTransaction>> ExecuteAsync(
            string inventoryName,
            DateTime? dateFrom,
            DateTime? dateTo,
            InventoryTransactionType? transactionType
            )
        {
            if(dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);

            return await this.inventoryTransactionRepository.GetInventoryTransactionAsync(
                inventoryName,
                dateFrom,
                dateTo,
                transactionType
                );

        }
    }
}
