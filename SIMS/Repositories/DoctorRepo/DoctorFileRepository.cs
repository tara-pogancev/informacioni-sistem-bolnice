using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.DoctorRepo
{
    class DoctorFileRepository : GenericFileRepository<string, Doctor, DoctorRepository>,IDoctorRepository
    {
       

        public List<string> getAllId()
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
                if (!retVal.Contains(l.SpecijalizacijaLekara))
                    retVal.Add(l.SpecijalizacijaLekara);
            }

            return retVal;
        }

        public List<string> GetAvailableSpecializationString()
        {
            List<String> retVal = new List<String>();

            foreach (Doctor l in base.GetAll())
            {
                if (!retVal.Contains(l.Specialization))
                    retVal.Add(l.Specialization);
            }

            return retVal;
        }

        public List<Doctor> ReadBySpecialization(Specialization specialization)
        {
            List<Doctor> retVal = new List<Doctor>();

            foreach (Doctor l in base.GetAll())
            {
                if (l.SpecijalizacijaLekara == specialization)
                    retVal.Add(l);
            }

            return retVal;
        }

        public Doctor ReadUser(string user)
        {
            foreach (Doctor l in this.GetAll())
            {
                if (l.KorisnickoIme == user)
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
            AppointmentRepository storageT = new AppointmentRepository();
            foreach (Appointment t in storageT.GetAll())
            {
                if (t.Lekar.Jmbg == key)
                {
                    storageT.Delete(t.TerminKey);
                }
            }
        }

    }
}
