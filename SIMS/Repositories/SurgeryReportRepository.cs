using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class SurgeryReportRepository : Repository<string, SurgeryReport, SurgeryReportRepository>
    {
        protected override string getKey(SurgeryReport entity)
        {
            return entity.OperacijaKey;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\operacijaIzvestaji.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        public List<SurgeryReport> ReadByPatient(Patient p)
        {
            List<SurgeryReport> retVal = new List<SurgeryReport>();

            foreach (SurgeryReport a in this.ReadList())
            {
                a.InitData();

                if (a.Operacija.Pacijent.Jmbg == p.Jmbg)
                    retVal.Add(a);
            }

            return retVal;
        }
    }
}
