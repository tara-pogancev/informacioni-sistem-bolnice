using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    public class RoomController
    {
        private readonly RoomService roomService = new RoomService();

        public List<Room> GetAllRooms() => roomService.GetAllRooms();

        public Room GetRoom(String key) => roomService.GetRoom(key);
        public void CreateOrUpdate(Room room)
        {
            roomService.CreateOrUpdate(room);
        }

        public void Delete(string Number)
        {
            roomService.Delete(Number);
        }
        public void Update(Room room)
        {
            roomService.Update(room);
        }

        public void MergeRooms(string room1, string room2)
        {
            roomService.MergeRooms(room1, room2);
        }

        public List<Room> GetAllHospitalizationRooms() => roomService.GetAllHospitalizationRooms();

        public List<Room> GetByType(RoomType roomType) => roomService.GetByType(roomType);

    }
}
