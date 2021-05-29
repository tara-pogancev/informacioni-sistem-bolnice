using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.PatientRepo
{
    interface IPatientRepository:IGenericRepository<Patient,String>
    {
        Patient ReadUser(String user);
    }
}
