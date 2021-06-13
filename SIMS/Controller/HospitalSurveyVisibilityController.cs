using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.Controller
{
    class HospitalSurveyVisibilityController
    {
        private HospitalSurveyVisiblityService hospitalSurveyVisibilityService;


        public HospitalSurveyVisibilityController()
        {
            hospitalSurveyVisibilityService = new HospitalSurveyVisiblityService();
        }

        public bool ShowSurveyToPatient(Patient patient) => hospitalSurveyVisibilityService.ShowSurveyToPatient(patient);
    }
}
