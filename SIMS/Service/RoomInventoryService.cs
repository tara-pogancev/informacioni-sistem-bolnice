using SIMS.Model;
using SIMS.Repositories.RoomInventoryRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Controller;

namespace SIMS.Service
{
    public class RoomInventoryService
    {
        private IRoomInventoryRepository roomInventoryRepository = new RoomInventoryFileRepository();

        internal void Update(string roomNumber, string inventoryID, string amountMoved)
        {
            roomInventoryRepository.Update(new RoomInventory(roomNumber, inventoryID, int.Parse(amountMoved)));
        }

        public bool GetIfAvailableBeds(Room room)
        {
            HospitalizationController hospitalizationController = new HospitalizationController();
            int takenBeds = 0;

            String id = room.Number + "_5";
            int availableBeds = roomInventoryRepository.FindById(id).Quantity; ;
            
            foreach (Hospitalization hospitalization in hospitalizationController.GetAllHospitalizations())
                if (hospitalization.Room.Number == room.Number)
                    takenBeds++;

            return (availableBeds > takenBeds);
        }
    }
}
