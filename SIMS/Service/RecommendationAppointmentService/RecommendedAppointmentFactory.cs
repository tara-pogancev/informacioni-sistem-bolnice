using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service.RecommendationAppointmentService
{

    class RecommendedAppointmentFactory
    {
        List<TimeSpan> HoursOfRecommendAppointment;
        List<RecommendedAppointmentDraft> possiblRecommendedAppointments;
        DateTime startDate;
        DateTime endDate;

        public RecommendedAppointmentFactory(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            HoursOfRecommendAppointment = new List<TimeSpan>() { new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0) };
            possiblRecommendedAppointments = new List<RecommendedAppointmentDraft>();
        }

        public List<RecommendedAppointmentDraft> getRecommendedAppointmentDrafts()
        {
            CreateAllPossibleRecommendedAppointments();
            return possiblRecommendedAppointments;
        }

        public void CreateAllPossibleRecommendedAppointments()
        {

            while (startDate <= endDate)
            {
                CreateStartTimeOfAppointment();
                startDate = startDate.AddDays(1);
            }
        }


        private void CreateStartTimeOfAppointment()
        {
            for (int i = 0; i < HoursOfRecommendAppointment.Count; i++)
            {
                possiblRecommendedAppointments.Add(new RecommendedAppointmentDraft(startDate + HoursOfRecommendAppointment[i]));
            }
        }


    }
}
