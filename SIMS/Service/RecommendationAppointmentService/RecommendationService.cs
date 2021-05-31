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
        private TypeOfRecommendation type;
        private string doctorID;
        private DateTime startDate;
        private DateTime endDate;
        List<Appointment> recommendedAppointments;
        private string patientID;
        private RecommendedAppointmentDTO recommendedAppointmentDto;

        public RecommendationService(RecommendedAppointmentDTO recommendedAppointmentDto)
        {
            this.recommendedAppointmentDto=recommendedAppointmentDto;
            this.type = recommendedAppointmentDto.Type;
            this.doctorID = recommendedAppointmentDto.DoctorID;
            this.startDate = recommendedAppointmentDto.StartDate;
            this.endDate =recommendedAppointmentDto.EndDate;
            this.patientID =recommendedAppointmentDto.PatientID;
            recommendedAppointments = new List<Appointment>();
        }

        public List<Appointment> GetRecommendedAppointments()
        {
            if (type == TypeOfRecommendation.DoctorRecommendation)
            {
                DoctorRecommendationPolicy doctorPolicy = new DoctorRecommendationPolicy(recommendedAppointmentDto);

                FillRecommendedAppointments(doctorPolicy.GetDoctorRecommendationDraft());
            }
            else
            {
                DateRecommendationPolicy dateRecommendationPolicy = new DateRecommendationPolicy(startDate, endDate, patientID);
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

                if (type == TypeOfRecommendation.DoctorRecommendation)
                {
                    recommendedAppointments.Add(new Appointment(appointment.TimeOfAppointment,
                                                            30,
                                                            AppointmentType.examination,
                                                            doctorRepository.FindById(doctorID),
                                                            patientRepository.FindById(patientID),
                                                            roomService.GetAvailableRooms(appointment.TimeOfAppointment)[0]));
                }
                else
                {
                    recommendedAppointments.Add(new Appointment(appointment.TimeOfAppointment,
                                                            30,
                                                            AppointmentType.examination,
                                                            doctorRepository.FindById(appointment.AvailableDoctorsID[0]),
                                                            patientRepository.FindById(patientID),
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
