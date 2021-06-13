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

        internal bool Update(string roomNumber, string inventoryID, string amountMoved)
        {
            try
            {
                roomInventoryRepository.Update(new RoomInventory(roomNumber, inventoryID, int.Parse(amountMoved)));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal void CreateOrUpdate(RoomInventory roomInventory)
        {
            roomInventoryRepository.CreateOrUpdate(roomInventory);
        }
        internal List<RoomInventory> ReadAll()
        {
            return roomInventoryRepository.GetAll();
        }

        public bool GetIfAvailableBeds(Room room)
        {
            // Vraca TRUE ukoliko data prostorija moze da primi jos jednog pacijenta, 
            // ili FALSE ukoliko ne moze

            HospitalizationController hospitalizationController = new HospitalizationController();
            int takenBeds = 0;
            int availableBeds = 0;

            String id = room.Number + "_5";

            if (roomInventoryRepository.FindById(id) == null)
                return false;

            availableBeds = roomInventoryRepository.FindById(id).Quantity;
            
            foreach (Hospitalization hospitalization in hospitalizationController.GetAllHospitalizations())
                if (hospitalization.Room.Number == room.Number)
                    takenBeds++;

            return (availableBeds > takenBeds);
        }

        public bool MoveInventory(string sourceRoomNumber, string destinationRoomNumber, string inventoryID, int amount)
        {
            if (sourceRoomNumber == destinationRoomNumber)
            {
                return true;
            }

            RoomInventory src = roomInventoryRepository.ReadNoConsistifying(sourceRoomNumber, inventoryID);
            RoomInventory dst = roomInventoryRepository.ReadNoConsistifying(destinationRoomNumber, inventoryID);

            if (src.Quantity < amount)
            {
                return false;
            }

            src.Quantity -= amount;
            dst.Quantity += amount;

            roomInventoryRepository.Update(src);
            roomInventoryRepository.Update(dst);

            return true;
        }
    }
}
