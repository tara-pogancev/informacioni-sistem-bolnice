using SIMS.Model;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.PatientRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service.RecommendationAppointmentService
{
    class DoctorRecommendationDraftsTransformation:ITransformDraftToAppointment
    {
        public List<Appointment> TransformDraftToAppointment(List<RecommendedAppointmentDraft> recommendedAppointmentsDraft, String patientID, String doctorID)
        {
            IDoctorRepository doctorRepository = new DoctorFileRepository();
            IPatientRepository patientRepository = new PatientFileRepository();
            RoomAvailabilityService roomService = new RoomAvailabilityService();
            List<Appointment> recommendedAppointments = new List<Appointment>();
            for (int i = 0; i < recommendedAppointmentsDraft.Count && i < 5; i++)
            {
                recommendedAppointments.Add(new Appointment(recommendedAppointmentsDraft[i].TimeOfAppointment,
                                                        30,
                                                        AppointmentType.examination,
                                                        doctorRepository.FindById(doctorID),
                                                        patientRepository.FindById(patientID),
                                                        roomService.GetAvailableRooms(recommendedAppointmentsDraft[i].TimeOfAppointment)[0]));

            }

            return recommendedAppointments;

        }
    }
}
