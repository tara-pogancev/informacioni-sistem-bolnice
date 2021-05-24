using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;

namespace SIMS.Service
{
    class DoctorRecommendationPolicy
    {
        
        DateTime startDate;
        DateTime endDate;
        String doctorID;
        String patientID;
        List<RecommendedAppointment> recommendedAppointementsDrafts;


        public DoctorRecommendationPolicy(DateTime startDate,DateTime endDate,String doctorID,String patientID)
        {
            this.recommendedAppointementsDrafts = new List<RecommendedAppointment>();
            this.startDate = startDate;
            this.endDate = endDate;
            this.doctorID = doctorID;
            this.patientID = patientID;
            recommendedAppointementsDrafts = new RecommendedAppointmentFactory(startDate, endDate).getRecommendedAppointmentDrafts();

        }

        private void RemoveReservedAppointments()
        {
            AppointmentService appointmentService= new AppointmentService();
            foreach(var appointment in appointmentService.GetAllAppointments())
            {
                if (appointment.Doctor.Jmbg==doctorID || appointment.Patient.Jmbg==patientID)
                    DeleteFromAppointmentsDraft(appointment);
            }

        }

        

        private void DeleteFromAppointmentsDraft(Appointment appointment)
        {
            RoomAvailabilityService roomAvailabilityService = new RoomAvailabilityService();
            for (int i = 0; i < recommendedAppointementsDrafts.Count; i++)
            {
                if (recommendedAppointementsDrafts[i].TimeOfAppointment==appointment.StartTime || !roomAvailabilityService.IsFreeRoom(recommendedAppointementsDrafts[i].TimeOfAppointment))
                    recommendedAppointementsDrafts.RemoveAt(i);
            }
        }

        public List<RecommendedAppointment> GetDoctorRecommendationDraft()
        {
            RemoveReservedAppointments();
            return recommendedAppointementsDrafts;
        }
    }
}
