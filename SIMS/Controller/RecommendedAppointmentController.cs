using SIMS.DTO;
using SIMS.Model;
using SIMS.Service.RecommendationAppointmentService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Controller
{
    class RecommendedAppointmentController
    {
        
        private RecommendationService recomendationService;
        public RecommendedAppointmentController()
        {
            recomendationService = new RecommendationService();
        }

        public List<Appointment> DateRecommendation(RecommendedAppointmentDTO recommendedAppointmentDTO)
        {
            recomendationService.SetRecommendationStrategy(new DateRecommendationPolicy(recommendedAppointmentDTO.StartDate, recommendedAppointmentDTO.EndDate, recommendedAppointmentDTO.PatientID));
            return recomendationService.GetRecommendedAppointments();
        }

        public List<Appointment> DoctorRecommendataion(RecommendedAppointmentDTO recommendedAppointmentDTO)
        {
            recomendationService.SetRecommendationStrategy(new DoctorRecommendationPolicy(recommendedAppointmentDTO));
            return recomendationService.GetRecommendedAppointments();
        }
    }
}
