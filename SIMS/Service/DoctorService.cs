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

    }
}
