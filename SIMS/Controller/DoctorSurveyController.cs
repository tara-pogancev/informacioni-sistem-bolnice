using SIMS.Model;
using SIMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class DoctorSurveyController
    {
        private DoctorSurveyService doctorSurveyService;

        public DoctorSurveyController()
        {
            doctorSurveyService = new DoctorSurveyService();
        }

        public List<DoctorSurvey> GetAllDoctorSurveys() => doctorSurveyService.GetAllDoctorSurveys();

        public void UpdateDoctorSurvey(DoctorSurvey doctorSurvey) => doctorSurveyService.Update(doctorSurvey);

        public void DeleteDoctorSurvey(String key) => doctorSurveyService.DeleteDoctorSurvey(key);

        public void SaveDoctorSurvey(DoctorSurvey doctorSurvey) => doctorSurveyService.SaveDoctorSurvey(doctorSurvey);

        public DoctorSurvey GetDoctorSurvey(String key) => doctorSurveyService.GetDoctorSurvey(key);

        public List<DoctorSurvey> GetSurveysForDoctor(Doctor doctor) => doctorSurveyService.GetSurveysForDocutr(doctor);
    }
}
