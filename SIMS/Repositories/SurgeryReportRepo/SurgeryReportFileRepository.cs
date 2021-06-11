using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.SurgeryReportRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.SurgeryReportRepo
{
    public class SurgeryReportFileRepository : GenericFileRepository<string, SurgeryReport, SurgeryReportFileRepository>,ISurgeryReportRepository
    {
        protected override string getKey(SurgeryReport entity)
        {
            return entity.ReportID;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\operacijaIzvestaji.json";
        }

        protected override void RemoveReferences(string key)
        {
            throw new NotImplementedException();
        }

        public List<SurgeryReport> ReadByPatient(Patient patient)
        {
            List<SurgeryReport> retVal = new List<SurgeryReport>();

            foreach (SurgeryReport a in this.GetAll())
            {
                if (a.GetSurgery() != null)
                    if (a.GetSurgery().Patient.Jmbg == patient.Jmbg)
                        retVal.Add(a);
            }

            return retVal;
        }

        protected override void ShouldSerialize(SurgeryReport entity)
        {
        }
    }
}
