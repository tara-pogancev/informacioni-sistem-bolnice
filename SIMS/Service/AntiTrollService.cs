using SIMS.Model;
using SIMS.Repositories.AppointmentLogRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class AntiTrollService
    {
        private IAppointmentLogRepository appointemntLogRepository;

        public AntiTrollService()
        {
            appointemntLogRepository = new AppointmentLogFileRepository();
        }

        public bool CanChangeAppointment(Patient patient)
        {
            int numberOFLogs = 0;
            List<AppointmentLog> terminLogs = new AppointmentLogService().GetPatientAppointmentLogs(patient);
            foreach (AppointmentLog terminLog in terminLogs)
            {
                if (terminLog.NotInLastTenDays())
                {
                    continue;
                }
                numberOFLogs++;
            }
            return numberOFLogs < 5;
        }

        public void BanUser(Patient patient)
        {
            patient.IsBanned = true;
            new PatientService().UpdatePatient(patient);
            new AppointmentLogService().MakeLogExpired(patient);
        }
    }
}
