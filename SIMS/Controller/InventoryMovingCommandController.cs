using SIMS.Daemon.PremestajOpreme;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class InventoryMovingCommandController
    {
        private InventoryMovingCommandService inventoryMovingCommandService = new InventoryMovingCommandService();
        public List<InventoryMovingCommand> ReadAll() => inventoryMovingCommandService.ReadAll();
        public void CreateOrUpdate(InventoryMovingCommand inventoryMovingCommand) => inventoryMovingCommandService.CreateOrUpdate(inventoryMovingCommand);
        public void Delete(InventoryMovingCommand inventoryMovingCommand) => inventoryMovingCommandService.Delete(inventoryMovingCommand);
    }
}
