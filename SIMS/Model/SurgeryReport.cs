using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;

namespace SIMS.Model
{
    public class SurgeryReport
    {
        public DateTime DatumIzvestaja { get; set; }
        public String OperacijaKey { get; set; }
        public String NazivOperacije { get; set; }
        public String NapomeneOperacije { get; set; }

        public SurgeryReport()
        {
            DatumIzvestaja = DateTime.Today;
        }

        public SurgeryReport(Appointment termin, String nazivOperacije, String napomeneOperacije)
        {
            Operacija = termin;

            DatumIzvestaja = DateTime.Today;
            NazivOperacije = nazivOperacije;
            NapomeneOperacije = napomeneOperacije;
            OperacijaKey = termin.AppointmentID;

        }

        public void InitData()
        {
            Operacija = new AppointmentFileRepository().FindById(OperacijaKey);
            Operacija.Patient = new PatientFileRepository().FindById(Operacija.Patient.Jmbg);
            Operacija.Doctor = new DoctorFileRepository().FindById(Operacija.Doctor.Jmbg);

        }

        [JsonIgnore]
        public String ImeLekara
        {
            get { return Operacija.GetDoctorName(); }
        }

        [JsonIgnore]
        public String ImePacijenta
        {
            get { return Operacija.GetPatientName(); }
        }

        [JsonIgnore]
        public String DatumRodjenjaPacijenta
        {
            get { return Operacija.Patient.GetDateOfBirthString(); }
        }

        [JsonIgnore]
        public String DatumOperacijeIspis
        {
            get { return Operacija.GetAppointmentDate(); }
        }

        [JsonIgnore]
        public String SobaOperacije
        {
            get { return Operacija.Room.Number; }
        }

        [JsonIgnore]
        public Appointment Operacija { get; set; }

    }
}
