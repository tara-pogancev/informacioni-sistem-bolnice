using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
