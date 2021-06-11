using SIMS.Model;
using SIMS.Repositories.HospitalSurveyRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Service.AppointmentServices;

namespace SIMS.Service
{
    class HospitalSurveyService
    {
        private IHospitalSurveyRepository hospitalSurveyRepository;
       

        public HospitalSurveyService()
        {
            hospitalSurveyRepository = new HospitalSurveyFileRepository();
            
        }

        public List<HospitalSurvey> GetAllHospitalSurveys() => hospitalSurveyRepository.GetAll();
       
        public void UpdateHospitalSurvey(HospitalSurvey hospitalSurvey)=>hospitalSurveyRepository.Update(hospitalSurvey);

        public void DeleteHospitalSurvey(String key) => hospitalSurveyRepository.Delete(key);

        public void SaveHospitalSurvey(HospitalSurvey hospitalSurvey)=>hospitalSurveyRepository.Save(hospitalSurvey);

        public HospitalSurvey GetHospitalSurvey(String key)=>hospitalSurveyRepository.FindById(key);

        public List<HospitalSurvey> GetPatientHospitalSurveys(Patient pacijent)
        {
            List<HospitalSurvey> anketeBolnice =hospitalSurveyRepository.GetAll();
            for (int i = 0; i < anketeBolnice.Count; i++)
            {
                if (anketeBolnice[i].IdVlasnika != pacijent.Jmbg)
                {
                    anketeBolnice.RemoveAt(i);
                    i--;
                }
            }
            return anketeBolnice;
        }
    }
}
