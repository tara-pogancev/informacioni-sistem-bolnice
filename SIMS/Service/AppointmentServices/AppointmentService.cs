using System;
using System.Collections.Generic;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;

namespace SIMS.Service.AppointmentServices
{
    
    public class AppointmentService 
    {
        private IAppointmentRepository appointmentRepository = new AppointmentFileRepository();
        
        
        private readonly DoctorAppointmentService doctorAppointmentService;


        public AppointmentService()
        {
            
        }

       

        public List<Appointment> GetAllAppointments() => appointmentRepository.GetAll();

        public void UpdateAppointment(Appointment appointment) => appointmentRepository.Update(appointment);

        public void DeleteAppointment(Appointment appointment) => appointmentRepository.Delete(appointment.AppointmentID);

        public void SaveAppointment(Appointment appointment) => appointmentRepository.Save(appointment);

        public Appointment GetAppointment(String key) => appointmentRepository.FindById(key);
    }
}
