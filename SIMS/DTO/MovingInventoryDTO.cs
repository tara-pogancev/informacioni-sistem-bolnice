using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.DTO
{
    public class MovingInventoryDTO
    {
        public MovingInventoryDTO(string sourceRoomNumber, string destinationRoomNumber, string amountToBeMoved, DateTime? executionTime, string inventoryID)
        {
            this.sourceRoomNumber = sourceRoomNumber;
            this.destinationRoomNumber = destinationRoomNumber;
            this.amountToBeMoved = amountToBeMoved;
            this.executionTime = executionTime;
            this.inventoryID = inventoryID;
        }

        public string sourceRoomNumber { get; set; }
        public string destinationRoomNumber { get; set; }
        public string amountToBeMoved { get; set; }
        public DateTime? executionTime { get; set; }
        public string inventoryID { get; set; }
    }
}
