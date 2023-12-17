﻿using GestaoEstoque.CoreBusiness;
using GestaoEstoque.UseCase.Interfaces;
using GestaoEstoque.UseCase.Inventories.InterfacesUseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoEstoque.UseCase.Inventories
{
    public class ViewInventoryByNameUseCase : IViewInventoryByNameUseCase
    {
        private readonly IInventoryRepository _inventoryRepository;
        public ViewInventoryByNameUseCase(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public async Task<IEnumerable<Inventory>> ExecuteAsync(string name = "")
        {
            return await _inventoryRepository.GetInventoriesByNameAsync(name);
        }
    }
}
