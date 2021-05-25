﻿using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.SecretaryRepo
{
    interface IMedicationRepository:IGenericRepository<Medication,String>
    {
        List<Medication> GetApprovedMedicine();
        List<Medication> GetMedicineWaitingForApproval();
        List<Medication> GetDeniedMedicine();
    }
}
