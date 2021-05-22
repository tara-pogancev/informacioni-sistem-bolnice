// File:    PacijentStorage.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:17:45 PM
// Purpose: Definition of Class PacijentStorage

using Newtonsoft.Json;
using SIMS.Repositories.AppointmentRepo;
using System;
using System.Collections.Generic;
using System.IO;

namespace SIMS.Repositories.PatientRepo
{
    public class PatientFileRepository : GenericFileRepository<string, Patient, PatientFileRepository>,IPatientRepository
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
            AppointmentFileRepository storageT = new AppointmentFileRepository();
            foreach (Appointment t in storageT.GetAll())
            {
                if (t.Pacijent.Jmbg == key)
                {
                    storageT.Delete(t.TerminKey);
                }
            }
        }

        public Patient ReadUser(String user)
        {
            foreach (Patient p in this.GetAll())
            {
                if (p.KorisnickoIme == user)
                    return p;
            }

            return null;
        }
    }
}