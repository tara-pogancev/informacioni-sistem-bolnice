using SIMS.Model;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class DoctorService
    {
        private IDoctorRepository doctorRepository; 

        public DoctorService()
        {
            doctorRepository = new DoctorFileRepository();
        }

        public List<Specialization> GetAvailableSpecialization()
        {
            return doctorRepository.GetAvailableSpecialization();
        }

        public List<string> GetAvailableSpecializationString()
        {
            return doctorRepository.GetAvailableSpecializationString();
        }

        public List<String> GetAllIds()
        {
            return doctorRepository.GetAllId();
        }

        public List<Doctor> ReadBySpecialization(Specialization specialization)
        {
            return doctorRepository.ReadBySpecialization(specialization);
        }

        public Doctor ReadUserByUsername(String username)
        {
            return doctorRepository.ReadUser(username);
        }

    }
}
