using SIMS.Repositories.PatientRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.PatientRepo
{
    interface IPatientRepository:IGenericRepository<Patient,String>
    {
        Patient ReadUser(String user);
    }
}
