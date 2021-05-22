using Newtonsoft.Json;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using SIMS.Repositories.DoctorSurveyRepo;
using System;
using System.Collections.Generic;
using System.Text;


namespace SIMS.Repositories.PatientRepo
{
    public class Anamnesis
    {       
        public Anamnesis()
        {
            Datum = DateTime.Today;
        }

        public Anamnesis(Appointment termin, String glavneTegobe, String sadasnjaAnamneza, String opstePojave, String respiratorniSistem, String kardiovaskularniSistem,
            String digestivniSistem, String urogenitalniSistem, String lokomotorniSistem, String nervniSistem, String ranijaOboljenja, String porodicniPodaci, String socioEpiPodaci)
        {

            Termin = termin;
            Datum = DateTime.Today;

            GlavneTegobe = glavneTegobe;
            SadasnjaAnamneza = sadasnjaAnamneza;
            OpstePojave = opstePojave;
            IdAnamneze = termin.TerminKey;

            if (respiratorniSistem == "") RespiratorniSistem = "/";
                else RespiratorniSistem = respiratorniSistem;

            if (kardiovaskularniSistem == "") KardiovaskularniSistem = "/";
                else KardiovaskularniSistem = kardiovaskularniSistem;

            if (digestivniSistem == "") DigestivniSistem = "/";
                else DigestivniSistem = digestivniSistem;

            if (urogenitalniSistem == "") UrogenitalniSistem = "/";
                else UrogenitalniSistem = urogenitalniSistem;

            if (lokomotorniSistem == "") LokomotorniSistem = "/";
                else LokomotorniSistem = lokomotorniSistem;

            if (nervniSistem == "") NervniSistem = "/";
                else NervniSistem = nervniSistem;

            if (ranijaOboljenja == "") RanijaOboljenja = "/";
                else RanijaOboljenja = ranijaOboljenja;

            if (porodicniPodaci == "") PorodicniPodaci = "/";
                else PorodicniPodaci = porodicniPodaci;

            if (socioEpiPodaci == "") SocioEpiPodaci = "/";
                else SocioEpiPodaci = socioEpiPodaci;
            
        }

        public Appointment getTermin()
        {
            return AppointmentFileRepository.Instance.FindById(Termin.TerminKey);
        }

        [JsonIgnore]
        public String ImeLekara
        {
            get { return Termin.ImeLekara; }
        }

        [JsonIgnore]
        public String ImePacijenta
        {
            get { return Termin.ImePacijenta; }
        }

        [JsonIgnore]
        public String Date
        {
            get { return Datum.ToString("dd.MM.yyyy."); }
        }

        [JsonIgnore]
        public String TerminDateType
        {
            get
            {
                if (getTermin().GetVrsta.Equals(AppointmentType.pregled))
                    return "Datum pregleda: " + getTermin().Datum;
                else return "Datum operacije: " + getTermin().Datum;

            }
        }

        //TODO get set za sva polja
        public String GlavneTegobe { get; set; }

        public String SadasnjaAnamneza { get; set; }

        public String OpstePojave { get; set; }

        public String RespiratorniSistem { get; set; }

        public String KardiovaskularniSistem { get; set; }

        public String DigestivniSistem { get; set; }

        public String UrogenitalniSistem { get; set; }

        public String LokomotorniSistem { get; set; }

        public String NervniSistem { get; set; }

        public String RanijaOboljenja { get; set; }

        public String PorodicniPodaci { get; set; }

        public String SocioEpiPodaci { get; set; }

        public DateTime Datum { get; set; }

        [JsonIgnore]
        public Appointment Termin { get; set; }

        public String IdAnamneze { get; set; }

        public void InitData()
        {
            Termin =  new AppointmentFileRepository().FindById(IdAnamneze);
            Termin.Pacijent = new PatientFileRepository().FindById(Termin.Pacijent.Jmbg);
            Termin.Lekar = new DoctorFileRepository().FindById(Termin.Lekar.Jmbg);
        }
    }
}
