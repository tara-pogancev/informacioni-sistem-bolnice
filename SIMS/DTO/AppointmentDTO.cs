using SIMS.Model;
using SIMS.Repositories.AnamnesisRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.DTO
{
    public class AppointmentDTO : Appointment
    {
        public AppointmentDTO()
        {
            InitData();
        }

        public AppointmentDTO(Appointment anamnesisAppointment) : base(anamnesisAppointment)
        {
            InitData();
        }

        public String AppointmentDate { get => GetAppointmentDate(); }

        public String AppointmentTime { get => GetAppointmentTime(); }

        public String RoomNumber { get => Room.Number; }

        public String AppointmentTypeString
        {
            get
            {
                if (Type == AppointmentType.examination)
                    return "Pregled";
                else
                    return "Operacija";
            }
        }

        public String AppointmentTypeAndDate
        {
            get
            {
                if (Type == AppointmentType.examination)
                    return "Datum pregleda: " + AppointmentDate;
                else 
                    return "Datum operacije: " + AppointmentDate;
            }
        }

        public String PatientName
        {
            get => Patient.FullName;
        }

        public String DoctorName
        {
            get => Doctor.FullName;
        }
    }
}
