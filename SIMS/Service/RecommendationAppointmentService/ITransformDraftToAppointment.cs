using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Service.RecommendationAppointmentService
{
    interface ITransformDraftToAppointment
    {
        public List<Appointment> TransformDraftToAppointment(List<RecommendedAppointmentDraft> recommendedAppointmentDrafts,String PatientID,String DoctorID);
    }
}
