using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class RoomAvailabilityService
    {
        private IRoomRepository roomRepository;
        


        public RoomAvailabilityService()
        {
            roomRepository = new RoomFileRepository();
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

        private void RemoveRoom(string number, List<Room> examRooms)
        {
            for (int i = 0; i < examRooms.Count; i++)
            {
                if (examRooms[i].Number == number)
                {
                    examRooms.RemoveAt(i);
                    i--;
                }
            }
        }

        public bool IsFreeRoom(DateTime appointmentTime)
        {
            return GetAvailableRooms(appointmentTime).Count != 0;
        }
    }
}
