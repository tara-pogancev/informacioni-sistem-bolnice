using System;
using System.Collections.Generic;
using SIMS.Model;
using SIMS.Service.AppointmentServices;

namespace SIMS.Service
{
    class HospitalSurveyVisiblityService
    {
        private HospitalSurveyService hospitalSurveyService;

        public HospitalSurveyVisiblityService()
        {
            this.hospitalSurveyService =new HospitalSurveyService();
        }

        public bool ShowSurveyToPatient(Patient patient)
        {
            List<HospitalSurvey> anketeBolnice = hospitalSurveyService.GetPatientHospitalSurveys(patient);
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
            return Math.Abs(hospitalSurvey.NumberOfCheckups - new PatientAppointmentsService().GetNumberOfFinishedAppointments(patient)) > 5;
        }
    }
}