using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Controller;
using SIMS.Model;

namespace SIMS.Service.OCPAppointment
{
    abstract class AppointmentScheduleAbstractService
    {
        // Abstraktna klasa koja dodeljuje bazne informacije neophodne za zakazivanje termina
        // OCP uočavamo u mogućnosti proširivanja za potencijalno nove tipove termina koji zahtevaju različite specijalizacije, 
        // tipove prostorija ili trajanje termina.

        private List<Specialization> validSpecialization;
        private List<int> validAppointmentDuration;
        private List<RoomType> validRoomType;

        public int GetDurationFromString(String duration)
        {
            // string template: X* minuta
            String[] subs = duration.Split(' ');
            String number = subs[0];
            return Int32.Parse(number); 
            
        }

        public List<Doctor> GetDoctorsForAppointment()
        {
            DoctorService doctorService = new DoctorService();
            List<Doctor> retVal = new List<Doctor>();

            foreach (Specialization specialization in validSpecialization)
                foreach (Doctor doctor in doctorService.ReadBySpecialization(specialization))
                    retVal.Add(doctor);

            retVal.Sort();
            return retVal;
        }
        
        public List<Room> GetRoomsForAppointment()
        {
            RoomService roomService = new RoomService();
            List<Room> retVal = new List<Room>();

            foreach (RoomType type in validRoomType)
                foreach (Room room in roomService.GetByType(type))
                    retVal.Add(room);

            return retVal;
        }

        public List<String> GetDurationList()
        {
            List<String> retVal = new List<string>();

            foreach (int duration in validAppointmentDuration)
            {
                String text = duration.ToString() + " minuta";
                retVal.Add(text);
            }

            return retVal;
        }

        protected abstract void SetSpecialization();

        protected abstract void SetDuration();

        protected abstract void SetRoomType();



    }
}
