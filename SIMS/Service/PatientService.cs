using SIMS.DTO;
using SIMS.Model;
using SIMS.Repositories.PatientRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    public class PatientService
    {
        private IPatientRepository patientRepositry;

        public PatientService()
        {
            patientRepositry = new PatientFileRepository();
        }

        public List<Patient> GetAllPatients() => patientRepositry.GetAll();

        public void UpdatePatient(Patient patient) => patientRepositry.Update(patient);

        public void DeletePatient(String patientKey) => patientRepositry.Delete(patientKey);

        public void SavePatient(Patient patient) => patientRepositry.Save(patient);

        public Patient GetPatient(String patientKey) => patientRepositry.FindById(patientKey);

        public PatientDTO GetDTO(Patient patient)
        {
            return new PatientDTO(patient);
        }

        public List<PatientDTO> GetDTOFromList(List<Patient> list)
        {
            List<PatientDTO> retVal = new List<PatientDTO>();
            foreach (Patient patient in list)
                retVal.Add(GetDTO(patient));

            return retVal;
        }


    }
}
