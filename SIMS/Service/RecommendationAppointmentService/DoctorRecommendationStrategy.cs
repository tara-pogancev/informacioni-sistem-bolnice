using SIMS.Model;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using SIMS.DTO;
using SIMS.Service.AppointmentServices;

namespace SIMS.Service.RecommendationAppointmentService
{
    class DoctorRecommendationStrategy:IRecommendationStrategy
    {

        DateTime startDate;
        DateTime endDate;
        string doctorID;
        string patientID;
        List<RecommendedAppointmentDraft> recommendedAppointementsDrafts;


        public DoctorRecommendationStrategy(RecommendedAppointmentDTO recommendedAppointmentDto)
        {
            recommendedAppointementsDrafts = new List<RecommendedAppointmentDraft>();
            this.startDate = recommendedAppointmentDto.StartDate;
            this.endDate =recommendedAppointmentDto.EndDate;
            this.doctorID =recommendedAppointmentDto.DoctorID;
            this.patientID = recommendedAppointmentDto.PatientID;
            recommendedAppointementsDrafts = new AppointmentDraftPreparation(startDate, endDate).GetRecommendedAppointmentDrafts();

        }

        private void RemoveReservedAppointments()
        {
            AppointmentService appointmentService = new AppointmentService();
            foreach (var appointment in appointmentService.GetFutureAppointments())
            {
                if (appointment.Doctor.Jmbg == doctorID || appointment.Patient.Jmbg == patientID)
                    DeleteFromAppointmentsDraft(appointment);
            }

        }

        
        private void DeleteFromAppointmentsDraft(Appointment appointment)
        {
            RoomAvailabilityService roomAvailabilityService = new RoomAvailabilityService();
            for (int i = 0; i < recommendedAppointementsDrafts.Count; i++)
            {
                if ( appointment.SameStartTime(recommendedAppointementsDrafts[i].TimeOfAppointment) || !roomAvailabilityService.IsFreeRoomExists(recommendedAppointementsDrafts[i].TimeOfAppointment))
                    recommendedAppointementsDrafts.RemoveAt(i);
            }
        }

        public List<Appointment> GetRecommendedAppointments()
        {
            RemoveReservedAppointments();
            ITransformDraftToAppointment transformDraftToAppointment = new DoctorRecommendationDraftsTransformation();
            return transformDraftToAppointment.TransformDraftToAppointment(recommendedAppointementsDrafts, patientID, doctorID);
        }
    }
}
