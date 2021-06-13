using SIMS.Daemon.PremestajOpreme;
using SIMS.Repositories.InventoryMovingCommandRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class InventoryMovingCommandService
    {
        private IInventoryMovingCommandRepository inventoryMovingCommandRepository = new InventoryMovingCommandFileRepository();
        public List<InventoryMovingCommand> ReadAll() => inventoryMovingCommandRepository.GetAll();
        public void CreateOrUpdate(InventoryMovingCommand inventoryMovingCommand) => inventoryMovingCommandRepository.CreateOrUpdate(inventoryMovingCommand);
        public void Delete(InventoryMovingCommand inventoryMovingCommand) => inventoryMovingCommandRepository.Delete(inventoryMovingCommand);
    }
}
