using SIMS.DTO;
using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class InventoryController
    {
        InventoryService inventoryService = new InventoryService();

        public void MoveInventory(MovingInventoryDTO dto)
        {
            inventoryService.MoveInventory(dto);
        }

        public void Delete(string ID)
        {
            inventoryService.Delete(ID);
        }

        public void CreateOrUpdate(Inventory inventory)
        {
            inventoryService.CreateOrUpdate(inventory);
        }

        public List<Inventory> GetAll()
        {
            return inventoryService.GetAll();
        }
    }
}
