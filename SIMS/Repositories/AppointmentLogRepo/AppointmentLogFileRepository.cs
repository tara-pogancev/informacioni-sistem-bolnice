using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class AppointmentLogFileRepository : GenericFileRepository<string, AppointmentLog, AppointmentLogFileRepository>
    {
        protected override string getKey(AppointmentLog entity)
        {
            return entity.AppointmentLogID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\terminLog.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        public List<AppointmentLog> ReadByPatient(Patient pacijent)
        {
            List<AppointmentLog> terminLogs = GetAll();
            for(int i = 0; i < terminLogs.Count; i++)
            {
                if (terminLogs[i].PatientID != pacijent.Jmbg || terminLogs[i].Expired==true)
                {
                    terminLogs.RemoveAt(i);
                    i--;
                }
            }
            return terminLogs;
        }

        public void MakeLogExpired(Patient pacijent)
        {
            List<AppointmentLog> terminLogs = ReadByPatient(pacijent);
            foreach(AppointmentLog terminLog in terminLogs)
            {
                terminLog.Expired = true;
                Update(terminLog);
            }
        }
    }
}
