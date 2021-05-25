using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Model;

namespace SIMS.DTO
{
    public class AnamnesisDTO : Anamnesis
    {
        public AnamnesisDTO()
        {
            InitData();
        }

        public AnamnesisDTO(Anamnesis anamnesis) : base(anamnesis)
        {

        }

        public AppointmentDTO AnamnesisAppointmentDTO
        {
            get
            {
                AppointmentDTO retVal = new AppointmentDTO(AnamnesisAppointment);
                return retVal;
            }
        }

        public String DoctorName
        {
            get { return AnamnesisAppointmentDTO.DoctorName; }
        }

        public String PatientName
        {
            get { return AnamnesisAppointmentDTO.PatientName; }
        }

        public String Date
        {
            get { return AnamnesisDate.ToString("dd.MM.yyyy."); }
        }

        public String AppointmentTypeAndDate
        {
            get
            {
                if (AnamnesisAppointmentDTO.AppointmentTypeString.Equals(AppointmentType.examination))
                    return "Datum pregleda: " + GetAppointment().GetAppointmentDate();
                else return "Datum operacije: " + GetAppointment().GetAppointmentDate();
            }
        }



    }
}
