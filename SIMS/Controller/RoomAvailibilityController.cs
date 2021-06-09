using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class RoomAvailibilityController
    {
        private RoomAvailabilityService roomAvailabilityService;

        public RoomAvailibilityController()
        {
            roomAvailabilityService = new RoomAvailabilityService();
        }

        public void Renovate(Room room) => roomAvailabilityService.Renovate(room);

        public List<Room> GetAvailableRooms(DateTime appointmentTime) => roomAvailabilityService.GetAvailableRooms(appointmentTime);

        public bool IsFreeRoomExists(DateTime appointmentTime) => roomAvailabilityService.IsFreeRoomExists(appointmentTime);

        public List<Room> GetRoomsWithRenovationInInterval(DateTime intervalStart, DateTime intervalEnd) => roomAvailabilityService.GetRoomsWithRenovationInInterval(intervalStart, intervalEnd);
    }
}
