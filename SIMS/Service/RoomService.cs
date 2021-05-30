using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.InventoryMovingCommandRepo;
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
                if (inventoryMovingCommand.DstID == room1)
                {
                    inventoryMovingCommandRepository.Delete(inventoryMovingCommand);
                    inventoryMovingCommand.DstID = room2;
                    inventoryMovingCommandRepository.Save(inventoryMovingCommand);
                }
                else if (inventoryMovingCommand.SrcID == room1)
                {
                    inventoryMovingCommandRepository.Delete(inventoryMovingCommand);
                    inventoryMovingCommand.SrcID = room2;
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
                if (roomInventory.BrojProstorije == room2)
                {
                    roomInventory.Kolicina += roomInventoryRepository.Read(room1, roomInventory.IdInventara).Kolicina;
                    roomInventoryRepository.Update(roomInventory);
                }
            }

            foreach (var roomInventory in roomInventoryRepository.GetAll())
            {
                if (roomInventory.BrojProstorije == room1)
                {
                    roomInventoryRepository.Delete(roomInventory);
                }
            }
        }
    }
}
