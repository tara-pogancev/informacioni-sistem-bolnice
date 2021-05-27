using SIMS.Model;
using SIMS.Repositories.HospitalSurveyRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

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

        public bool ShowSurveyToPatient(Patient patient)
        {
            List<HospitalSurvey> anketeBolnice = new HospitalSurveyService().GetPatientHospitalSurveys(patient);
            if (anketeBolnice.Count == 0 || IsFiveAppointmentsPassed(anketeBolnice[anketeBolnice.Count - 1],patient) || IsThreeMonthsPassed(anketeBolnice[anketeBolnice.Count - 1]))
            {
                return true;
            }
            return false;
        }

        private bool IsThreeMonthsPassed(HospitalSurvey hospitalSurvey)
        {
            return hospitalSurvey.SubmissionDate.AddMonths(3)<DateTime.Now;
        }
        private bool IsFiveAppointmentsPassed(HospitalSurvey hospitalSurvey,Patient patient)
        {
            return Math.Abs(hospitalSurvey.NumberOfCheckups - new AppointmentService().GetNumberOfFinishedAppointments(patient)) > 5;
        }
    }
}
