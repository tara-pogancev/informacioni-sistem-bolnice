using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;
using SIMS.Service.AppointmentServices;

namespace SIMS.Service.RecommendationAppointmentService
{
    class DateRecommendationStrategy:IRecommendationStrategy
    {
        
        string patientID;
        List<RecommendedAppointmentDraft> recommendedAppointementsDrafts;


        public DateRecommendationStrategy(DateTime startDate, DateTime endDate, string patientID)
        {
            recommendedAppointementsDrafts = new List<RecommendedAppointmentDraft>();
            
            this.patientID = patientID;
            recommendedAppointementsDrafts = new AppointmentDraftPreparation(startDate, endDate).GetRecommendedAppointmentDrafts();
        }

        private void RemoveBusyDoctors(Appointment appointment)
        {

            for (int i = 0; i < recommendedAppointementsDrafts.Count; i++)
            {

                if (recommendedAppointementsDrafts[i].TimeOfAppointment.Equals(appointment.StartTime))
                {
                    recommendedAppointementsDrafts[i].AvailableDoctorsID.Remove(appointment.Doctor.Jmbg);

                }

            }
        }

        private void RemovePatientAppointments(Appointment appointment)
        {
            RoomAvailabilityService roomAvailability = new RoomAvailabilityService();
            for (int i = 0; i < recommendedAppointementsDrafts.Count; i++)
            {
                if (recommendedAppointementsDrafts[i].TimeOfAppointment.Equals(appointment.StartTime) && appointment.Patient.Jmbg == patientID || !roomAvailability.IsFreeRoomExists(recommendedAppointementsDrafts[i].TimeOfAppointment))
                {
                    recommendedAppointementsDrafts.RemoveAt(i);
                    i--;
                }
            }
        }

        private void RemoveReservedAppointments()
        {
            List<Appointment> scheduledAppointments = new AppointmentService().GetFutureAppointments();
            for (int i = 0; i < scheduledAppointments.Count; i++)
            {

                RemoveBusyDoctors(scheduledAppointments[i]);
                RemovePatientAppointments(scheduledAppointments[i]);

            }
            RemoveAppointmentsWithoutDoctors();
        }

        private void RemoveAppointmentsWithoutDoctors()
        {
            for (int i = 0; i < recommendedAppointementsDrafts.Count; i++)
            {
                if (recommendedAppointementsDrafts[i].AvailableDoctorsID.Count == 0)
                {
                    recommendedAppointementsDrafts.RemoveAt(i);
                    i--;
                }
            }
        }

        public List<Appointment> GetRecommendedAppointments()
        {
            RemoveReservedAppointments();
            ITransformDraftToAppointment transformDraftToAppointment = new DateRecommendationDraftsTransformation();
            return transformDraftToAppointment.TransformDraftToAppointment(recommendedAppointementsDrafts, patientID, "");
           
        }
    }
}
