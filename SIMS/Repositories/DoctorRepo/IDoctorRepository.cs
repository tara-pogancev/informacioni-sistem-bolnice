using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.DoctorRepo
{
    interface IDoctorRepository:IGenericRepository<Doctor,String>
    {
        Doctor ReadUser(String user);
        List<String> getAllId();
        List<Doctor> ReadBySpecialization(Specialization specialization);
        List<Specialization> GetAvailableSpecialization();
        List<String> GetAvailableSpecializationString();
    }
}
