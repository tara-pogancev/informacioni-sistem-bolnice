using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service.RecommendationAppointmentService
{

    class RecommendedAppointmentFactory
    {
        List<TimeSpan> HoursOfRecommendAppointment;
        List<RecommendedAppointmentDraft> PossibleRecommendation;
        DateTime startDate;
        DateTime endDate;

        public RecommendedAppointmentFactory(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            HoursOfRecommendAppointment = new List<TimeSpan>() { new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0) };
            PossibleRecommendation = new List<RecommendedAppointmentDraft>();
        }

        public List<RecommendedAppointmentDraft> getRecommendedAppointmentDrafts()
        {
            CreateAllPossibleRecommendedAppointments();
            return PossibleRecommendation;
        }

        private void CreateAllPossibleRecommendedAppointments()
        {
            int performanceConstraint = 0;
            while (startDate <= endDate && performanceConstraint<5)
            {
                CreateStartTimeOfAppointment();
                startDate = startDate.AddDays(1);
                performanceConstraint++;
            }
        }


        private void CreateStartTimeOfAppointment()
        {
            for (int i = 0; i < HoursOfRecommendAppointment.Count; i++)
            {
                PossibleRecommendation.Add(new RecommendedAppointmentDraft(startDate + HoursOfRecommendAppointment[i]));
            }
        }


    }
}
