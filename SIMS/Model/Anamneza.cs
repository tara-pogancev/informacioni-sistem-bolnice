using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Anamneza
    {       
        public Anamneza()
        {
            Datum = DateTime.Today;
        }

        public Anamneza(Termin termin, String glavneTegobe, String sadasnjaAnamneza, String opstePojave, String respiratorniSistem, String kardiovaskularniSistem,
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

        public Termin getTermin()
        {
            return TerminStorage.Instance.Read(Termin.TerminKey);
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

                if (getTermin().GetVrsta.Equals(TipTermina.pregled))
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
        public Termin Termin { get; set; }

        public String IdAnamneze { get; set; }

        public void InitData()
        {
            Termin =  new TerminStorage().Read(IdAnamneze);
            Termin.Pacijent = new PacijentStorage().Read(Termin.Pacijent.Jmbg);
            Termin.Lekar = new LekarStorage().Read(Termin.Lekar.Jmbg);
        }
    }
}
