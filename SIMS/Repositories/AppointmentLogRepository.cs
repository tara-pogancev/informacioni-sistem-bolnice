using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class AppointmentLogRepository : Repository<string, AppointmentLog, AppointmentLogRepository>
    {
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

        public List<AppointmentLog> ReadByPatient(Patient pacijent)
        {
            List<AppointmentLog> terminLogs = ReadList();
            for(int i = 0; i < terminLogs.Count; i++)
            {
                if (terminLogs[i].PacijentKey != pacijent.Jmbg || terminLogs[i].Istekao==true)
                {
                    terminLogs.RemoveAt(i);
                    i--;
                }
            }
            return terminLogs;
        }

        public void logoviIstekli(Patient pacijent)
        {
            List<AppointmentLog> terminLogs = ReadByPatient(pacijent);
            foreach(AppointmentLog terminLog in terminLogs)
            {
                terminLog.Istekao = true;
                Update(terminLog);
            }
        }
    }
}
