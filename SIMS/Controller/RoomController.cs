using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class RoomController
    {
        RoomService roomService = new RoomService();
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
