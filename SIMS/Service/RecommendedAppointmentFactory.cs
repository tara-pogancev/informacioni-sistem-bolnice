using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service  
{

    class RecommendedAppointment
    {
        
        private List<String> availableDoctorsID;
        private DateTime timeOfAppointment;
        public RecommendedAppointment()
        {

        }
        public RecommendedAppointment(DateTime vrijeme)
        {
            this.timeOfAppointment = vrijeme;
            availableDoctorsID = new DoctorFileRepository().GetAllId();
        }

        public List<String> AvailableDoctorsID { get => availableDoctorsID; set => availableDoctorsID = value; }
        public DateTime TimeOfAppointment { get => timeOfAppointment; set => timeOfAppointment = value; }
    }

    class RecommendedAppointmentFactory
    {
        List<TimeSpan> HoursOfRecommendAppointment;
        List<RecommendedAppointment> possiblRecommendedAppointments;
        DateTime startDate;
        DateTime endDate;

        public RecommendedAppointmentFactory(DateTime startDate,DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            HoursOfRecommendAppointment = new List<TimeSpan>(){ new TimeSpan(8, 0, 0), new TimeSpan(9, 0, 0),new TimeSpan(10,0,0) };
            possiblRecommendedAppointments = new List<RecommendedAppointment>();
        }

        public List<RecommendedAppointment> getRecommendedAppointmentDrafts()
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
                possiblRecommendedAppointments.Add(new RecommendedAppointment(startDate + HoursOfRecommendAppointment[i]));
            }
        }


    }
}
