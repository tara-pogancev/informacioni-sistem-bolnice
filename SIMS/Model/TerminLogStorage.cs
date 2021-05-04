using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    class TerminLogStorage : Storage<string, TerminLog, TerminLogStorage>
    {
        protected override string getKey(TerminLog entity)
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

        public List<TerminLog> ReadByPatient(Pacijent pacijent)
        {
            List<TerminLog> terminLogs = ReadList();
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

        public void logoviIstekli(Pacijent pacijent)
        {
            List<TerminLog> terminLogs = ReadByPatient(pacijent);
            foreach(TerminLog terminLog in terminLogs)
            {
                terminLog.Istekao = true;
                Update(terminLog);
            }
        }
    }
}
