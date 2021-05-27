using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    public class RoomController
    {
        RoomService roomService = new RoomService();

        public RoomController()
        {

        }

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
    }
}
