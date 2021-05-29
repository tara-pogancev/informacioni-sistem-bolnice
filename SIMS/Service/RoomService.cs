using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class RoomService
    {
        private IRoomRepository roomRepository = new RoomFileRepository();
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

        public List<Room> GetAllRooms()
        {
            return roomRepository.GetAll();
        }
    }
}
