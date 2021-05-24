using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class PatientService
    {
        private IPatientRepository patientRepositry;

        public PatientService()
        {
            patientRepositry = new PatientFileRepository();
        }

        public void UpdatePatient(Patient patient)=>patientRepositry.Update(patient);

        public void DeletePatient(String patientKey) => patientRepositry.Delete(patientKey);

        public void SavePatient(Patient patient) => patientRepositry.Save(patient);

        public Patient GetPatient(String patientKey) => patientRepositry.FindById(patientKey);


    }
}
