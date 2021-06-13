using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;

namespace SIMS.Service.RecommendationAppointmentService
{
    class RecommendedAppointmentDraft
    {

        private List<string> availableDoctorsID;
        private DateTime timeOfAppointment;
        public RecommendedAppointmentDraft()
        {

        }
        public RecommendedAppointmentDraft(DateTime time)
        {
            timeOfAppointment = time;
            availableDoctorsID = new DoctorService().GetExaminationDoctorsID();
        }

        public List<string> AvailableDoctorsID { get => availableDoctorsID; set => availableDoctorsID = value; }
        public DateTime TimeOfAppointment { get => timeOfAppointment; set => timeOfAppointment = value; }
    }
}
