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
            OperacijaKey = termin.TerminKey;

        }

        public void InitData()
        {
            Operacija = new AppointmentFileRepository().FindById(OperacijaKey);
            Operacija.Pacijent = new PatientFileRepository().FindById(Operacija.Pacijent.Jmbg);
            Operacija.Lekar = new DoctorFileRepository().FindById(Operacija.Lekar.Jmbg);

        }

        [JsonIgnore]
        public String ImeLekara
        {
            get { return Operacija.ImeLekara; }
        }

        [JsonIgnore]
        public String ImePacijenta
        {
            get { return Operacija.ImePacijenta; }
        }

        [JsonIgnore]
        public String DatumRodjenjaPacijenta
        {
            get { return Operacija.Pacijent.DatumString; }
        }

        [JsonIgnore]
        public String DatumOperacijeIspis
        {
            get { return Operacija.Datum; }
        }

        [JsonIgnore]
        public String SobaOperacije
        {
            get { return Operacija.NazivProstorije; }
        }

        [JsonIgnore]
        public Appointment Operacija { get; set; }

    }
}
