using SIMS.Repositories.PatientRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.AnamnesisRepository
{
    interface IAnamnesisRepository:IGenericRepository<Anamnesis,String>
    {
        List<Anamnesis> ReadByPatient(Patient patient);
    }
}
