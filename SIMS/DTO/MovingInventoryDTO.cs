using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.DTO
{
    public class MovingInventoryDTO
    {
        public MovingInventoryDTO(string sourceRoomNumber, string destinationRoomNumber, string amountToBeMoved, DateTime? executionTime, string inventoryID)
        {
            this.SourceRoomNumber = sourceRoomNumber;
            this.DestinationRoomNumber = destinationRoomNumber;
            this.AmountToBeMoved = amountToBeMoved;
            this.ExecutionTime = executionTime;
            this.InventoryID = inventoryID;
        }

        public string SourceRoomNumber { get; set; }
        public string DestinationRoomNumber { get; set; }
        public string AmountToBeMoved { get; set; }
        public DateTime? ExecutionTime { get; set; }
        public string InventoryID { get; set; }
    }
}
