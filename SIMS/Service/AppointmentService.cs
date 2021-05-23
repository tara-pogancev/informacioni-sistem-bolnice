using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    public class AppointmentService
    {
        private IAppointmentRepository appointmentRepository;

        public AppointmentService()
        {
            appointmentRepository = new AppointmentFileRepository();
            
        }
        //Staviti DTO object
        public List<String> GetAvailableTimeOfAppointment(Doctor doctor,String date,Patient patient)
        {
            List<String> timeOfAppointment = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            foreach (Appointment appointment in GetScheduledAppointments(date))
            {
                if (doctor.Unavailable(appointment) || patient.Unvailable(appointment)) 
                {
                    timeOfAppointment.Remove(appointment.Vrijeme);
                }
            }
            return timeOfAppointment;
        }

        //TODO staviti da baca excetion
        public bool ScheduleAppointment(Doctor doctor,DateTime date,Patient patient)
        {
            RoomAvailabilityService roomAvailabilityService = new RoomAvailabilityService();
            List<Room> availableRooms = roomAvailabilityService.GetAvailableRooms(date);
            if (availableRooms.Count == 0)
            {
                return false;
            }

            appointmentRepository.Save(new Appointment(date, 30, AppointmentType.pregled, doctor, patient, availableRooms[0]));
            return true;
        }

        public void changeAppointment()
        {

        }


        private List<Appointment> GetScheduledAppointments(String date)
        {
            List<Appointment> scheduledAppointments = appointmentRepository.GetAll();
            for (int i = 0; i < scheduledAppointments.Count;i++)
            {
                    if (scheduledAppointments[i].Datum != date)
                    {
                        scheduledAppointments.RemoveAt(i);
                        i--;
                    }
            }
            return scheduledAppointments;
        }
       
    }
}
