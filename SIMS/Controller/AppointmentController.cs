using SIMS.DTO;
using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Service.AppointmentServices;

namespace SIMS.Controller
{
   

    public class AppointmentController
    {
        private AppointmentService appointmentService;
        


        public AppointmentController()
        {
            appointmentService = new AppointmentService();
            
        }
        


        public List<Appointment> GetAllAppointments()
        {
            return appointmentService.GetAllAppointments();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            appointmentService.UpdateAppointment(appointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            appointmentService.DeleteAppointment(appointment);
        }

        public void SaveAppointment(Appointment appointment)
        {
            appointmentService.SaveAppointment(appointment);
        }

        public Appointment GetAppointment(String key)
        {
            return appointmentService.GetAppointment(key);
        }
    }
}
