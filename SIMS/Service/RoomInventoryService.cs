using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    public class RoomInventoryService
    {
        private IRoomInventoryRepository roomInventoryRepository = new RoomInventoryFileRepository();

        internal void Update(string roomNumber, string inventoryID, string amountMoved)
        {
            roomInventoryRepository.Update(new RoomInventory(roomNumber, inventoryID, int.Parse(amountMoved)));
        }
    }
}
