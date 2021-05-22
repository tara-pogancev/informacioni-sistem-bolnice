using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.SecretaryRepo
{
    interface IPatientRepository:IGenericRepository<Patient,String>
    {
        Patient ReadUser(String user);
    }
}
