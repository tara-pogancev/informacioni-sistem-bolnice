using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.PatientRepo
{
    interface IMedicationRepository:IGenericRepository<Medication,String>
    {
        List<Medication> getApprovedMedicine();
        List<Medication> getMedicineWaitingForApproval();
        List<Medication> getDeniedMedicine();
    }
}
