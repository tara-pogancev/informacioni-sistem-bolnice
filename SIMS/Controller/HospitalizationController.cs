using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service;

namespace SIMS.Controller
{
    public class HospitalizationController
    {
        private HospitalizationService hospitalizationService = new HospitalizationService();

        public HospitalizationController()
        {

        }

        public List<Hospitalization> GetAllHospitalizations() => hospitalizationService.GetAllHospitalizations();

        public void UpdateHospitalization(Hospitalization hospitalization) => hospitalizationService.UpdateHospitalization(hospitalization);

        public void DeleteHospitalization(Hospitalization hospitalization) => hospitalizationService.DeleteHospitalization(hospitalization);

        public void SaveHospitalization(Hospitalization hospitalization) => hospitalizationService.SaveHospitalization(hospitalization);

        public Hospitalization GetHospitalization(String key) => hospitalizationService.GetHospitalization(key);

        public Boolean GetIfPatientHospitalzied(Patient patient) => hospitalizationService.GetIfPatientHospitalzied(patient);

        public List<Hospitalization> GetCurrentHospitalizations() => hospitalizationService.GetCurrentHospitalizations();

        public Hospitalization GetPatientCurrentHospitalization(Patient patient) => hospitalizationService.GetPatientCurrentHospitalization(patient);

    }
}
