using System;
using System.Collections.Generic;
using System.Text;
using SIMS.DTO;
using SIMS.Model;
using SIMS.Service;

namespace SIMS.Controller
{
    public class PatientController
    {
        private PatientService patientService;

        public PatientController()
        {
            patientService = new PatientService();
        }

        public List<Patient> GetAllPatients() => patientService.GetAllPatients();

        public void UpdatePatient(Patient patient) => patientService.UpdatePatient(patient);

        public void DeletePatient(String patientKey) => patientService.DeletePatient(patientKey);

        public void SavePatient(Patient patient) => patientService.SavePatient(patient);

        public Patient GetPatient(String patientKey) => patientService.GetPatient(patientKey);

        public List<Patient> GetAllPatients()
        {
            return patientService.GetAllPatients();
        }

        public PatientDTO GetDTO(Patient patient)
        {
            return patientService.GetDTO(patient);
        }

        public List<PatientDTO> GetDTOFromList(List<Patient> list)
        {
            return patientService.GetDTOFromList(list);
        }



    }
}
