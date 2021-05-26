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

        public RoomService()
        {

        }

        public Room GetRoom(String key) => roomRepository.FindById(key);

        public List<Room> GetAllRooms() => roomRepository.GetAll();

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
    }
}
