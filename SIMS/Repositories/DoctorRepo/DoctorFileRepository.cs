using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories.DoctorRepo
{
    class DoctorFileRepository : GenericFileRepository<string, Doctor, DoctorRepository>,IDoctorRepository
    {
        public void Delete(string key)
        {
            this.DeleteEntity(key);
        }

        public Doctor FindById(string key)
        {
            return this.ReadEntity(key);
        }

        public IEnumerable<Doctor> GetAll()
        {
            return this.ReadEntities();
        }


        public void Save(Doctor entity)
        {
            this.CreateEntity(entity);
        }

        public void Update(Doctor entity)
        {
            this.UpdateEntity(entity);
        }

        public IEnumerable<string> getAllId()
        {
            List<String> ids = new List<String>();
            List<Doctor> lekari = this.ReadEntities();
            foreach (Doctor l in lekari)
            {
                ids.Add(l.Jmbg);
            }
            return ids;
        }

        public IEnumerable<Specialization> GetAvailableSpecialization()
        {
            List<Specialization> retVal = new List<Specialization>();

            foreach (Doctor l in ReadEntities())
            {
                if (!retVal.Contains(l.SpecijalizacijaLekara))
                    retVal.Add(l.SpecijalizacijaLekara);
            }

            return retVal;
        }

        public IEnumerable<string> GetAvailableSpecializationString()
        {
            List<String> retVal = new List<String>();

            foreach (Doctor l in ReadEntities())
            {
                if (!retVal.Contains(l.Specialization))
                    retVal.Add(l.Specialization);
            }

            return retVal;
        }

        public IEnumerable<Doctor> ReadBySpecialization(Specialization specialization)
        {
            List<Doctor> retVal = new List<Doctor>();

            foreach (Doctor l in ReadEntities())
            {
                if (l.SpecijalizacijaLekara == specialization)
                    retVal.Add(l);
            }

            return retVal;
        }

        public Doctor ReadUser(string user)
        {
            foreach (Doctor l in this.ReadEntities())
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
            foreach (Appointment t in storageT.ReadEntities())
            {
                if (t.Lekar.Jmbg == key)
                {
                    storageT.DeleteEntity(t.TerminKey);
                }
            }
        }

    }
}
