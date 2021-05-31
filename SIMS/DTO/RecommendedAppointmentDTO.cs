using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.DTO
{
    class RecommendedAppointmentDTO
    {
        public TypeOfRecommendation Type { get; set; }
        public string DoctorID { get; set; }
        public DateTime StartDate { get; set; }
        public  DateTime EndDate { get; set; }
        public  string PatientID { get; set; }

        public RecommendedAppointmentDTO(TypeOfRecommendation type, String doctorId, DateTime startDate,
            DateTime endDate, String patientId)
        {
            Type = type;
            DoctorID = doctorId;
            StartDate = startDate;
            EndDate = endDate;
            PatientID = patientId;
        }
    }
}
