using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.CoreBusiness
{
    public class InventoryTransaction
    {
        public int InventoryTransactionId { get; set; }
        public string PONumber { get; set; } = string.Empty;
        public string ProductionNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo de id de inventário é obrigatório")]
        public int InventoryId { get; set; }

        [Required(ErrorMessage ="Campo de quantidade antes é obrigatório")]
        public int QuantityBefore { get; set; }


        [Required(ErrorMessage = "Necessário informar o tipo de transação")]
        public InventoryTransactionType ActivityType { get; set; }

        [Required(ErrorMessage = "Campo de quantidade depois é obrigatório")]
        public int QuantityAfter { get; set; }
        public double UnitPrice { get; set; }

        [Required(ErrorMessage = "Campo de data é obrigatório")]
        public DateTime TransactionDate { get; set; }

        [Required(ErrorMessage = "Necessário informar quem realizou a compra")]
        public string DoneBy { get; set; } = string.Empty;

        public Inventory? Inventory { get; set; }
    }
}
