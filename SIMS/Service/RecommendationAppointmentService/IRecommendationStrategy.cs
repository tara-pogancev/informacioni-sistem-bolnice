using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service.RecommendationAppointmentService
{
    interface IRecommendationStrategy
    {
        public List<Appointment> GetRecommendedAppointments();
    }
}
