using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service;
using SIMS.Service.AppointmentServices;

namespace SIMS.Controller
{
    public class ScheduleAppointmentControler
    {
        private ScheduleAppointmentService scheduleAppointmentService;

        public ScheduleAppointmentControler()
        {
            scheduleAppointmentService = new ScheduleAppointmentService();
        }

        public List<String> GetAvailableTimeOfAppointment(Doctor doctor, String date, Patient patient)
        {
            return scheduleAppointmentService.GetAvailableTimeOfAppointment(doctor, date, patient);
        }

        public bool ScheduleAppointment(Doctor doctor, DateTime date, Patient patient)
        {
            return scheduleAppointmentService.ScheduleAppointment(doctor, date, patient);
        }

        public void CancelAppointment(Appointment appointment)
        {
            scheduleAppointmentService.CancelAppointment(appointment);
        }

        public void ChangeAppointment(Appointment appointment)
        {
            scheduleAppointmentService.ChangeAppointment(appointment);
        }
    }
}
