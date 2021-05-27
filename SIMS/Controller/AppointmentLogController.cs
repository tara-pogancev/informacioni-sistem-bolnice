using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class AppointmentLogController
    {
        private AppointmentLogService appointmentLogService;

        public AppointmentLogController()
        {
            appointmentLogService = new AppointmentLogService();
        }

        public List<AppointmentLog> GetAllAppointmentLogs() => appointmentLogService.GetAllAppointmentsLogs();

        public void Update(AppointmentLog appointmentLog) => appointmentLogService.Update(appointmentLog);

        public void Delete(String key) => appointmentLogService.Delete(key);

        public void Save(AppointmentLog appointmentLog) => appointmentLogService.Save(appointmentLog);

        public AppointmentLog GetAppointmentLog(String key) => appointmentLogService.GetAppointmentLog(key);

        public void MakeLogExpired(Patient patient) => appointmentLogService.MakeLogExpired(patient);

        public List<AppointmentLog> GetPatientAppointmentLogs(Patient patient) => appointmentLogService.GetPatientAppointmentLogs(patient);
    }

}
