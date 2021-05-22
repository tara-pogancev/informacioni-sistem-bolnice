using Model;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AppointmentLogRepo
{
    class AppointmentLogFileRepository : GenericFileRepository<string, AppointmentLog, AppointmentLogRepository>, IAppointmentLogRepository
    {
        

        public IEnumerable<AppointmentLog> GetPatientLogs(Patient patient)
        {
            List<AppointmentLog> appointmentLogs = this.GetAll();
            for (int i = 0; i < appointmentLogs.Count; i++)
            {
                if (patient.EqualJmbg(appointmentLogs[i].PacijentKey) || appointmentLogs[i].Istekao == true)
                {
                    appointmentLogs.RemoveAt(i);
                    i--;
                }
            }
            return appointmentLogs;
        }


        protected override string getKey(AppointmentLog entity)
        {
            return entity.TerminLogKey;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\terminLog.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }
    }
}
