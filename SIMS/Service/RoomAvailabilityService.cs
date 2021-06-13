using SIMS.Exceptions;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.RoomRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class RoomAvailabilityService
    {
        private IRoomRepository roomRepository;
        private IAppointmentRepository appointmentRepository;
        

        public RoomAvailabilityService()
        {
            roomRepository = new RoomFileRepository();
            appointmentRepository = new AppointmentFileRepository();
        }


        public List<Room> GetAvailableRooms(DateTime appointmentTime)
        {
            List<Room> examRooms = roomRepository.UcitajProstorijeZaPreglede();
            foreach (Appointment termin in new AppointmentFileRepository().GetAll())
            {
                if (termin.EqualDate(appointmentTime))
                {
                    RemoveRoom(termin.Room.Number,examRooms);
                }
            }
            return examRooms;
            
        }

        public List<Room> GetRoomsWithRenovationInInterval(DateTime intervalStart, DateTime intervalEnd)
        {
            List<Room> rooms = new List<Room>();
            foreach (Room room in roomRepository.GetAll())
            {
                if (RoomIsRenovatedInInterval(room, intervalStart, intervalEnd))
                {
                    rooms.Add(room);
                }           
            }
            return rooms;
        }

        private bool RoomIsRenovatedInInterval(Room room, DateTime intervalStart, DateTime intervalEnd)
        {
            return (room.RenovationStart > intervalStart && room.RenovationStart < intervalEnd) || (room.RenovationEnd > intervalStart && room.RenovationEnd < intervalEnd);
        }

        private void RemoveRoom(string number, List<Room> examRooms)
        {
            for (int i = 0; i < examRooms.Count; i++)
            {
                if (examRooms[i].Number == number || examRooms[i].Available==false)
                {
                    examRooms.RemoveAt(i);
                    i--;
                }
            }
        }

        public bool IsFreeRoomExists(DateTime appointmentTime)
        {
            return GetAvailableRooms(appointmentTime).Count != 0;
        }

        private bool RenovationAppointmentOverlapped(Room room, Appointment appointment)
        {
            bool sameRoom = room.Number == appointment.Room.Number;
            bool startOverlap = room.RenovationStart > appointment.StartTime && room.RenovationStart < appointment.GetEndTime();
            bool endOverlap = room.RenovationEnd > appointment.StartTime && room.RenovationStart < appointment.GetEndTime();

            return sameRoom && (startOverlap || endOverlap);
        }

        public void Renovate(Room room)
        {
            foreach (var appointment in appointmentRepository.GetAll())
            {
                if (RenovationAppointmentOverlapped(room, appointment))
                {
                    throw new RenovationAppointmentOverlapException();
                }
            }
            roomRepository.Update(room);
        }
    }
}
