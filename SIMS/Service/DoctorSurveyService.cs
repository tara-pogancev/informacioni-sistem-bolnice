using SIMS.Model;
using SIMS.Repositories.DoctorSurveyRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class DoctorSurveyService
    {

        private IDoctorSurveyRepository doctorSurveyRepository;

        public DoctorSurveyService()
        {
            doctorSurveyRepository = new DoctorSurveyFileRepository();
        }

        public List<DoctorSurvey> GetAllDoctorSurveys() => doctorSurveyRepository.GetAll();

        public void Update(DoctorSurvey doctorSurvey) => doctorSurveyRepository.Update(doctorSurvey);

        public void DeleteDoctorSurvey(String key) => doctorSurveyRepository.Delete(key);
        
        public void SaveDoctorSurvey(DoctorSurvey doctorSurvey)=>doctorSurveyRepository.Save(doctorSurvey);

        public DoctorSurvey GetDoctorSurvey(String key) => doctorSurveyRepository.FindById(key);

        public List<DoctorSurvey> GetSurveysForDocutr(Doctor doctor) => doctorSurveyRepository.GetSurveysForDoctor(doctor);
    }
}
