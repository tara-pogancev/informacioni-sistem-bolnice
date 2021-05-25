using SIMS.Model;
using SIMS.Repositories.AppointmentLogRepo;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class AppointmentLogService
    {
        private IAppointmentLogRepository appointmentLogRepository;

        public AppointmentLogService()
        {
            appointmentLogRepository = new AppointmentLogFileRepository();
        }

        public List<AppointmentLog> GetAllAppointmentsLogs()=>appointmentLogRepository.GetAll();
        public void Update(AppointmentLog appointmentLog)=>appointmentLogRepository.Update(appointmentLog);
        public void Delete(String key)=>appointmentLogRepository.Delete(key);
        public void Save(AppointmentLog appointmentLog)=> appointmentLogRepository.Save(appointmentLog);
        public AppointmentLog GetAppointmentLog(String key)=>appointmentLogRepository.FindById(key);


        public void MakeLogExpired(Patient pacijent)
        {
            
            List<AppointmentLog> terminLogs = GetPatientAppointmentLogs(pacijent);
            foreach (AppointmentLog terminLog in terminLogs)
            {
                terminLog.Expired = true;
                appointmentLogRepository.Update(terminLog);
            }
        }

        public List<AppointmentLog> GetPatientAppointmentLogs(Patient pacijent)
        {
            List<AppointmentLog> terminLogs = appointmentLogRepository.GetAll();
            for (int i = 0; i < terminLogs.Count; i++)
            {
                if (terminLogs[i].PatientID != pacijent.Jmbg || terminLogs[i].Expired == true)
                {
                    terminLogs.RemoveAt(i);
                    i--;
                }
            }
            return terminLogs;
        }
    }
}
