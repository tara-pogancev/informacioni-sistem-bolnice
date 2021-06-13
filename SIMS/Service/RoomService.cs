using SIMS.Exceptions;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.InventoryMovingCommandRepo;
using SIMS.Repositories.RoomInventoryRepo;
using SIMS.Repositories.RoomRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class RoomService
    {
        private readonly IRoomRepository roomRepository = new RoomFileRepository();
        private readonly IInventoryMovingCommandRepository inventoryMovingCommandRepository = new InventoryMovingCommandFileRepository();
        private readonly IRoomInventoryRepository roomInventoryRepository = new RoomInventoryFileRepository();
        private readonly IAppointmentRepository appointmentRepository = new AppointmentFileRepository();
		
        public Room GetRoom(String key) => roomRepository.FindById(key);
		
        public List<Room> GetAllRooms() => roomRepository.GetAll();

        public List<Room> GetAllHospitalizationRooms()
        {
            List<Room> retVal = new List<Room>();
            foreach (Room room in GetAllRooms())
                if (room.RoomType == RoomType.patientRoom)
                    retVal.Add(room);

            return retVal;

        }

        public void CreateOrUpdate(Room room)
        {
            roomRepository.CreateOrUpdate(room);
        }

        public void Delete(string Number)
        {
            roomRepository.Delete(Number);
        }

        public void Update(Room room)
        {
            roomRepository.Update(room);
        }


        public void MergeRooms(string room1, string room2)
        {
            MergeInventoryMovingCommands(room1, room2);
            MergeRoomInventories(room1, room2);
            MergeAppointments(room1, room2);
            roomRepository.Delete(room1);
        }

        private void MergeInventoryMovingCommands(string room1, string room2)
        {
            foreach (var inventoryMovingCommand in inventoryMovingCommandRepository.GetAll())
            {
                if (inventoryMovingCommand.DestinationRoomNumber == room1)
                {
                    inventoryMovingCommandRepository.Delete(inventoryMovingCommand);
                    inventoryMovingCommand.DestinationRoomNumber = room2;
                    inventoryMovingCommandRepository.Save(inventoryMovingCommand);
                }
                else if (inventoryMovingCommand.SourceRoomNumber == room1)
                {
                    inventoryMovingCommandRepository.Delete(inventoryMovingCommand);
                    inventoryMovingCommand.SourceRoomNumber = room2;
                    inventoryMovingCommandRepository.Save(inventoryMovingCommand);
                }
            }
        }

        private void MergeAppointments(string room1, string room2)
        {
            foreach (var appointment in appointmentRepository.GetAll())
            {
                if (appointment.Room.Number == room1)
                {
                    appointmentRepository.Delete(appointment);
                    appointment.Room = roomRepository.FindById(room2);
                    appointmentRepository.Save(appointment);
                }
            }
        }

        private void MergeRoomInventories(string room1, string room2)
        {
            foreach (var roomInventory in roomInventoryRepository.GetAll())
            {
                if (roomInventory.RoomNumber == room2)
                {
                    roomInventory.Quantity += roomInventoryRepository.Read(room1, roomInventory.ID).Quantity;
                    roomInventoryRepository.Update(roomInventory);
                }
            }

            foreach (var roomInventory in roomInventoryRepository.GetAll())
            {
                if (roomInventory.RoomNumber == room1)
                {
                    roomInventoryRepository.Delete(roomInventory);
                }
            }
        }

        public List<Room> GetByType(RoomType roomType)
        {
            List<Room> retVal = new List<Room>();

            foreach (Room room in GetAllRooms())
                if (room.RoomType == roomType)
                    retVal.Add(room);

            return retVal;
        }
    }
}
