using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.SurgeryReportRepo
{
    interface ISurgeryReportRepository:IGenericRepository<SurgeryReport,String>
    {
        List<SurgeryReport> ReadByPatient(Patient p);
    }
}
