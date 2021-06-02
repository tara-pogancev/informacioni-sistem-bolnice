using System;
using System.Collections.Generic;
using System.Text;
using SIMS.Service;

namespace SIMS.Model
{
    public class Hospitalization
    {
        public Patient Patient { get; set; }
        public Doctor LeadDoctor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Room Room { get; set; }
        public String HospitalizationID { get; set; }

        public Hospitalization()
        {
            HospitalizationID = DateTime.Now.ToString("ddMMyyyyHHmmss");
        }

        public Hospitalization(Patient patient, Doctor doctor, DateTime start, DateTime end, Room room)
        {
            Patient = patient;
            LeadDoctor = doctor;
            StartDate = start;
            EndDate = end;
            Room = room;
            HospitalizationID = DateTime.Now.ToString("ddMMyyyyHHmmss");
        }

        public void InitData()
        {
            DoctorService doctorService = new DoctorService();
            PatientService patientService = new PatientService();
            RoomService roomService = new RoomService();

            Room = roomService.GetRoom(Room.Number);
            LeadDoctor = doctorService.GetDoctor(LeadDoctor.Jmbg);
            Patient = patientService.GetPatient(Patient.Jmbg);

        }

        public String GetStartDateString()
        {
            return StartDate.ToString("dd.MM.yyyy.");
        }

        public String GetEndDateString()
        {
            return EndDate.ToString("dd.MM.yyyy.");
        }

    }
}
