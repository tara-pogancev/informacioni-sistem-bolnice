using SIMS.Model;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.PatientRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using SIMS.DTO;

namespace SIMS.Service.RecommendationAppointmentService
{
    class RecommendationService
    {
        
        List<Appointment> recommendedAppointments;
 
        private RecommendedAppointmentDTO recommendedAppointmentDto;

        public RecommendationService(RecommendedAppointmentDTO recommendedAppointmentDto)
        {
            this.recommendedAppointmentDto=recommendedAppointmentDto;
            
            recommendedAppointments = new List<Appointment>();
        }

        public List<Appointment> GetRecommendedAppointments()
        {
            if (recommendedAppointmentDto.Type == TypeOfRecommendation.DoctorRecommendation)
            {
                DoctorRecommendationPolicy doctorPolicy = new DoctorRecommendationPolicy(recommendedAppointmentDto);

                FillRecommendedAppointments(doctorPolicy.GetDoctorRecommendationDraft());
            }
            else
            {
                DateRecommendationPolicy dateRecommendationPolicy = new DateRecommendationPolicy(recommendedAppointmentDto.StartDate, recommendedAppointmentDto.EndDate, recommendedAppointmentDto.PatientID);
                FillRecommendedAppointments(dateRecommendationPolicy.GetDateRecommendationAppointmentDraft());
            }
            return recommendedAppointments;
        }

        private void FillRecommendedAppointments(List<RecommendedAppointmentDraft> recommendedAppointmentsDraft)
        {
            IDoctorRepository doctorRepository = new DoctorFileRepository();
            IPatientRepository patientRepository = new PatientFileRepository();
            RoomAvailabilityService roomService = new RoomAvailabilityService();
            int counterOfAppointment = 0;
            foreach (var appointment in recommendedAppointmentsDraft)
            {

                if (recommendedAppointmentDto.Type == TypeOfRecommendation.DoctorRecommendation)
                {
                    recommendedAppointments.Add(new Appointment(appointment.TimeOfAppointment,
                                                            30,
                                                            AppointmentType.examination,
                                                            doctorRepository.FindById(recommendedAppointmentDto.DoctorID),
                                                            patientRepository.FindById(recommendedAppointmentDto.PatientID),
                                                            roomService.GetAvailableRooms(appointment.TimeOfAppointment)[0]));
                }
                else
                {
                    recommendedAppointments.Add(new Appointment(appointment.TimeOfAppointment,
                                                            30,
                                                            AppointmentType.examination,
                                                            doctorRepository.FindById(appointment.AvailableDoctorsID[0]),
                                                            patientRepository.FindById(recommendedAppointmentDto.PatientID),
                                                            roomService.GetAvailableRooms(appointment.TimeOfAppointment)[0]));
                }

                counterOfAppointment++;
                if (counterOfAppointment == 5)
                {
                    break;
                }

            }


        }
    }
}
