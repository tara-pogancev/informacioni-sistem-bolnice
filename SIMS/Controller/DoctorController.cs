using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Service;
using SIMS.Model;

namespace SIMS.Controller
{
    public class DoctorController
    {
        private DoctorService doctorService;

        public DoctorController()
        {
            doctorService = new DoctorService();
        }

        public List<Specialization> GetAvailableSpecialization()
        {
            return doctorService.GetAvailableSpecialization();
        }

        public List<string> GetAvailableSpecializationString()
        {
            return doctorService.GetAvailableSpecializationString();
        }

        public List<String> GetAllIds()
        {
            return doctorService.GetAllIds();
        }

        public List<Doctor> ReadBySpecialization(Specialization specialization)
        {
            return doctorService.ReadBySpecialization(specialization);
        }

        public Doctor ReadUserByUsername(String username)
        {
            return doctorService.ReadUserByUsername(username);
        }
    }
}
