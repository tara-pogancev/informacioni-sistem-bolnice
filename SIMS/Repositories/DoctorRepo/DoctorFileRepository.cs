using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Repositories.DoctorRepo
{
    class DoctorFileRepository : GenericFileRepository<string, Doctor, DoctorFileRepository>, IDoctorRepository
    {
        public List<string> GetAllId()
        {
            List<String> ids = new List<String>();
            List<Doctor> lekari = this.GetAll();
            foreach (Doctor l in lekari)
            {
                ids.Add(l.Jmbg);
            }
            return ids;
        }

        public List<Specialization> GetAvailableSpecialization()
        {
            List<Specialization> retVal = new List<Specialization>();

            foreach (Doctor l in base.GetAll())
            {
                if (!retVal.Contains(l.DoctorSpecialization))
                    retVal.Add(l.DoctorSpecialization);
            }

            return retVal;
        }

        public List<string> GetAvailableSpecializationString()
        {
            List<String> retVal = new List<String>();

            foreach (Doctor l in base.GetAll())
            {
                if (!retVal.Contains(l.SpecializationString))
                    retVal.Add(l.SpecializationString);
            }

            return retVal;
        }

        public List<Doctor> ReadBySpecialization(Specialization specialization)
        {
            List<Doctor> retVal = new List<Doctor>();

            foreach (Doctor l in base.GetAll())
            {
                if (l.DoctorSpecialization == specialization)
                    retVal.Add(l);
            }

            return retVal;
        }

        public Doctor ReadUser(string user)
        {
            foreach (Doctor l in this.GetAll())
            {
                if (l.Username == user)
                    return l;
            }

            return null;
        }

        protected override string getKey(Doctor entity)
        {
            return entity.Jmbg;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\lekari.json";
        }

        protected override void RemoveReferences(string key)
        {
            IAppointmentRepository storageT = new AppointmentFileRepository();
            foreach (Appointment t in storageT.GetAll())
            {
                if (t.Doctor.Jmbg == key)
                {
                    storageT.Delete(t.AppointmentID);
                }
            }
        }

    }
}
