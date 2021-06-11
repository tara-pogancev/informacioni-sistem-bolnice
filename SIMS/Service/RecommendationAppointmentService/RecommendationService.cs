using SIMS.Model;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.PatientRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.DTO;

namespace SIMS.Service.RecommendationAppointmentService
{
    class RecommendationService
    {
        IRecommendationStrategy recommendationStrategy;

        public RecommendationService()
        {
            
            
        }

        public List<Appointment> GetRecommendedAppointments()
        {
            return recommendationStrategy.GetRecommendedAppointments();
        }

        public void SetRecommendationStrategy(IRecommendationStrategy recommendationStrategy)
        {
            this.recommendationStrategy = recommendationStrategy;
        }

       

        }
}

