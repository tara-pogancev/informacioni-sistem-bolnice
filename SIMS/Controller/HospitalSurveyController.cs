using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class HospitalSurveyController
    {
        private HospitalSurveyService hospitalSurveyService;

        public HospitalSurveyController()
        {
            hospitalSurveyService = new HospitalSurveyService();
        }

        public List<HospitalSurvey> GetAllHospitalSurveys() => hospitalSurveyService.GetAllHospitalSurveys();

        public void UpdateHospitalSurvey(HospitalSurvey hospitalSurvey) => hospitalSurveyService.UpdateHospitalSurvey(hospitalSurvey);

        public void DeleteHospitalSurvey(String key) => hospitalSurveyService.DeleteHospitalSurvey(key);

        public void SaveHospitalSurvey(HospitalSurvey hospitalSurvey) => hospitalSurveyService.SaveHospitalSurvey(hospitalSurvey);

        public HospitalSurvey GetHospitalSurvey(String key) => hospitalSurveyService.GetHospitalSurvey(key);

        public List<HospitalSurvey> GetPatientHospitalSurveys(Patient pacijent) => hospitalSurveyService.GetPatientHospitalSurveys(pacijent);

        
    }
}
