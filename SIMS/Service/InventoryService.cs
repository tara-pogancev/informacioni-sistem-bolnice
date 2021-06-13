using SIMS.Daemon.PremestajOpreme;
using SIMS.DTO;
using SIMS.Model;
using SIMS.Repositories.InventoryRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class InventoryService
    {
        IInventoryRepository inventoryRepository = new InventoryFileRepository();
        public bool MoveInventory(MovingInventoryDTO dto)
        {
            int amountToBeMoved;

            try
            {
                amountToBeMoved = int.Parse(dto.AmountToBeMoved);
            }
            catch (Exception)
            {
                return false;
            }

            DateTime timeOfExecution;

            if (dto.ExecutionTime == null)
            {
                timeOfExecution = DateTime.Now;
            }
            else
            {
                timeOfExecution = (DateTime)dto.ExecutionTime;
            }

            InventoryMovingQueue.Instance.PushCommand(new InventoryMovingCommand(timeOfExecution, dto.SourceRoomNumber, dto.DestinationRoomNumber, dto.InventoryID, amountToBeMoved));
            return true;
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
