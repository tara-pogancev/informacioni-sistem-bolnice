using SIMS.Daemon.PremestajOpreme;
using SIMS.DTO;
using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class InventoryService
    {
        IInventoryRepository inventoryRepository = new InventoryFileRepository();
        public void MoveInventory(MovingInventoryDTO dto)
        {
            int amountToBeMoved = int.Parse(dto.amountToBeMoved);

            DateTime timeOfExecution;

            if (dto.executionTime == null)
            {
                timeOfExecution = DateTime.Now;
            }
            else
            {
                timeOfExecution = (DateTime)dto.executionTime;
            }

            InventoryMovingQueue.Instance.PushCommand(new InventoryMovingCommand(timeOfExecution, dto.sourceRoomNumber, dto.destinationRoomNumber, dto.inventoryID, amountToBeMoved));

        }

        public void Delete(string ID)
        {
            inventoryRepository.Delete(ID);
        }

        public void CreateOrUpdate(Inventory inventory)
        {
            inventoryRepository.CreateOrUpdate(inventory);
        }

        public List<Inventory> GetAll()
        {
            return inventoryRepository.GetAll();
        }
    }
}
