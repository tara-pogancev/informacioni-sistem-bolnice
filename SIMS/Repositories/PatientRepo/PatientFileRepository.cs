// File:    PacijentStorage.cs
// Author:  paracelsus
// Created: Monday, March 22, 2021 7:17:45 PM
// Purpose: Definition of Class PacijentStorage

using Newtonsoft.Json;
using SIMS.Repositories.AppointmentRepo;
using System;
using System.Collections.Generic;
using System.IO;
using SIMS.Model;

namespace SIMS.Repositories.SecretaryRepo
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
                if (t.Patient.Jmbg == key)
                {
                    storageT.Delete(t.AppointmentID);
                }
            }
        }

        public Patient ReadUser(String user)
        {
            foreach (Patient p in this.GetAll())
            {
                if (p.Username == user)
                    return p;
            }

            return null;
        }

        protected override void shouldSerialize(Patient entity)
        {
            entity.Serialize = true;
        }
    }
}