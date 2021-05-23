using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.SurgeryReportRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class SurgeryReportFileRepository : GenericFileRepository<string, SurgeryReport, SurgeryReportFileRepository>,ISurgeryReportRepository
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

            foreach (SurgeryReport a in this.GetAll())
            {
                a.InitData();

                if (a.Operacija.Patient.Jmbg == p.Jmbg)
                    retVal.Add(a);
            }

            return retVal;
        }
    }
}
