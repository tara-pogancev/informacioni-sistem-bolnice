// File:    PacijentStorage.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:17:45 PM
// Purpose: Definition of Class PacijentStorage

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Model
{
    public class PatientRepository : Repository<string, Patient, PatientRepository>
    {
        protected override string getKey(Patient entity)
        {
            return entity.Jmbg;
        }

        protected override string getPath()
        {
            return @".\..\..\..\Data\pacijenti.json";
        }

        protected override void RemoveReferences(string key)
        {
            AppointmentRepository storageT = new AppointmentRepository();
            foreach (Appointment t in storageT.ReadList())
            {
                if (t.Pacijent.Jmbg == key)
                {
                    storageT.Delete(t.TerminKey);
                }
            }
        }

        public Patient ReadUser(String user)
        {
            foreach (Patient p in this.ReadList())
            {
                if (p.KorisnickoIme == user)
                    return p;
            }

            return null;
        }
    }
}