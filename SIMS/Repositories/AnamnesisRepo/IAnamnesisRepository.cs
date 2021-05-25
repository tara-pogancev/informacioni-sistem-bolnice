using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.AnamnesisRepository
{
    interface IAnamnesisRepository:IGenericRepository<Anamnesis,String>
    {
        List<Anamnesis> ReadByPatient(Patient patient);
    }
}
