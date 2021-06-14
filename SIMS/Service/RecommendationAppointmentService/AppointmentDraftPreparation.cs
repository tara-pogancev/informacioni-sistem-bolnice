using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service.RecommendationAppointmentService
{

    class AppointmentDraftPreparation
    {
        List<TimeSpan> HoursOfRecommendAppointment;
        DateTime startDate;
        DateTime endDate;

        public AppointmentDraftPreparation(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            HoursOfRecommendAppointment = new List<TimeSpan>() { new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0) };
        }

        public List<RecommendedAppointmentDraft> GetRecommendedAppointmentDrafts()
        {
            List<RecommendedAppointmentDraft> possibleRecommendation = new List<RecommendedAppointmentDraft>();
            int performanceConstraint = 0;
            while (startDate <= endDate && performanceConstraint++ < 5)
            {
                CreateStartTimeOfAppointment(possibleRecommendation);
                startDate = startDate.AddDays(1);
            }
            return possibleRecommendation;
        }

        private void CreateStartTimeOfAppointment(List<RecommendedAppointmentDraft> possibleRecommendation)
        {
            for (int i = 0; i < HoursOfRecommendAppointment.Count; i++)
            {
                possibleRecommendation.Add(new RecommendedAppointmentDraft(startDate + HoursOfRecommendAppointment[i]));
            }
        }


    }
}
