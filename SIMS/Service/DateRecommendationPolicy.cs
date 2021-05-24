using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service
{
    class DateRecommendationPolicy
    {
        DateTime startDate;
        DateTime endDate;
        String doctorID;
        String patientID;
        List<RecommendedAppointment> recommendedAppointementsDrafts;

        
        public DateRecommendationPolicy(DateTime startDate, DateTime endDate, String patientID) 
        {
            this.recommendedAppointementsDrafts = new List<RecommendedAppointment>();
            this.startDate = startDate;
            this.endDate = endDate;
            this.patientID = patientID;
            recommendedAppointementsDrafts = new RecommendedAppointmentFactory(startDate, endDate).getRecommendedAppointmentDrafts();
        }

        private void RemoveBusyDoctors(Appointment termin)
        {

            for (int i = 0; i < recommendedAppointementsDrafts.Count; i++)
            {

                if (recommendedAppointementsDrafts[i].TimeOfAppointment.Equals(termin.StartTime))
                {
                    recommendedAppointementsDrafts[i].AvailableDoctorsID.Remove(termin.Doctor.Jmbg);
                    
                }

            }
        }

        private void RemovePatientAppointments(Appointment termin)
        {
            RoomAvailabilityService roomAvailability = new RoomAvailabilityService();
            for (int i = 0; i < recommendedAppointementsDrafts.Count; i++)
            {
                if ((recommendedAppointementsDrafts[i].TimeOfAppointment.Equals(termin.StartTime) && termin.Patient.Jmbg==(patientID)) || !roomAvailability.IsFreeRoom(recommendedAppointementsDrafts[i].TimeOfAppointment))
                {
                    recommendedAppointementsDrafts.RemoveAt(i);
                    i--;
                }
            }
        }

        private void RemoveReservedAppointments()
        {
            List<Appointment> scheduledAppointments = new AppointmentService().GetAllAppointments();
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {

                RemoveBusyDoctors(scheduledAppointments[i]);
                RemovePatientAppointments(scheduledAppointments[i]);

            }
            RemoveAppointmentsWithoutDoctors();
        }

        private void RemoveAppointmentsWithoutDoctors()
        {
            for(int i = 0; i < recommendedAppointementsDrafts.Count; i++)
            {
                if (recommendedAppointementsDrafts[i].AvailableDoctorsID.Count == 0)
                {
                    recommendedAppointementsDrafts.RemoveAt(i);
                    i--;
                }
            }
        }

        public List<RecommendedAppointment> GetDateRecommendationAppointmentDraft()
        {
            RemoveReservedAppointments();
            return recommendedAppointementsDrafts;
        }
    }
}
