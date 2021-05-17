// File:    LekarStorage.cs
// Author:  paracelsus
// Created: Friday, March 26, 2021 4:38:57 PM
// Purpose: Definition of Class LekarStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class DoctorRepository : Repository<string, Doctor, DoctorRepository>
    {
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
            foreach (Appointment t in storageT.ReadList())
            {
                if (t.Lekar.Jmbg == key)
                {
                    storageT.Delete(t.TerminKey);
                }
            }
        }

        public Doctor ReadUser(String user)
        {
            foreach(Doctor l in this.ReadList())
            {
                if (l.KorisnickoIme == user)
                    return l;
            }

            return null;
        }

        public List<String> getAllId()
        {
            List<String> ids = new List<String>();
            List<Doctor> lekari = this.ReadList();
            foreach(Doctor l in lekari)
            {
                ids.Add(l.Jmbg);
            }
            return ids;
        }

        public List<Doctor> ReadBySpecialization(Specialization specialization)
        {
            List<Doctor> retVal = new List<Doctor>();

            foreach(Doctor l in ReadList())
            {
                if (l.SpecijalizacijaLekara == specialization)
                    retVal.Add(l);
            }

            return retVal;
        }

        public List<Specialization> GetAvailableSpecialization()
        {
            List<Specialization> retVal = new List<Specialization>();

            foreach (Doctor l in ReadList())
            {
                if (!retVal.Contains(l.SpecijalizacijaLekara))
                    retVal.Add(l.SpecijalizacijaLekara);
            }

            return retVal;
        }

        public List<String> GetAvailableSpecializationString()
        {
            List<String> retVal = new List<String>();

            foreach (Doctor l in ReadList())
            {
                if (!retVal.Contains(l.Specialization))
                    retVal.Add(l.Specialization);
            }

            return retVal;
        }

    }
}