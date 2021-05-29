using SIMS.Model;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;

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

        public RecommendationService(TypeOfRecommendation type, string doctorID, DateTime startDate, DateTime endDate, string patientID)
        {
            this.type = type;
            this.doctorID = doctorID;
            this.startDate = startDate;
            this.endDate = endDate;
            this.patientID = patientID;
            recommendedAppointments = new List<Appointment>();
        }

        public List<Appointment> GetRecommendedAppointments()
        {
            if (type == TypeOfRecommendation.DoctorRecommendation)
            {
                DoctorRecommendationPolicy doctorPolicy = new DoctorRecommendationPolicy(startDate, endDate, doctorID, patientID);

                FillRecommendedAppointments(doctorPolicy.GetDoctorRecommendationDraft());
            }
            else
            {
                FillRecommendedAppointments(new DateRecommendationPolicy(startDate, endDate, patientID).GetDateRecommendationAppointmentDraft());
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
