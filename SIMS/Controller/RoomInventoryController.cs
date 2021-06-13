using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    public class RoomInventoryController
    {
        RoomInventoryService roomInventoryService = new RoomInventoryService();

        internal void Update(string roomNumber, string inventoryID, string amountMoved)
        {
            roomInventoryService.Update(roomNumber, inventoryID, amountMoved);
        }

        public bool GetIfAvailableBeds(Room room) => roomInventoryService.GetIfAvailableBeds(room);
        public bool MoveInventory(string sourceRoomNumber, string destinationRoomNumber, string inventoryID, int amount) => roomInventoryService.MoveInventory(sourceRoomNumber, destinationRoomNumber, inventoryID, amount);

        
    }
}
