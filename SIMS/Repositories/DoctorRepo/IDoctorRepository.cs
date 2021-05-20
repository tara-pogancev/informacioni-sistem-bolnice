using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.DoctorRepo
{
    interface IDoctorRepository:IGenericRepository<Doctor,String>
    {
        Doctor ReadUser(String user);
        IEnumerable<String> getAllId();
        IEnumerable<Doctor> ReadBySpecialization(Specialization specialization);
        IEnumerable<Specialization> GetAvailableSpecialization();
        IEnumerable<String> GetAvailableSpecializationString();
    }
}
