using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Anamneza
    {
        private String terminKey;
        private DateTime datum;

        private String glavneTegobe;
        private String sadasnjaAnamneza;
        private String opstePojave;
        private String respiratorniSistem;
        private String kardiovaskularniSistem;
        private String digestivniSistem;
        private String urogenitalniSistem;
        private String lokomotorniSistem;
        private String nervniSistem;
        private String ranijaOboljenja;
        private String porodicniPodaci;
        private String socioEpiPodaci;

        public Anamneza()
        {
            this.datum = DateTime.Today;
        }

        public Anamneza(String terminKey, String glavneTegobe, String sadasnjaAnamneza, String opstePojave, String respiratorniSistem, String kardiovaskularniSistem,
            String digestivniSistem, String urogenitalniSistem, String lokomotorniSistem, String nervniSistem, String ranijaOboljenja, String porodicniPodaci, String socioEpiPodaci)
        {
            this.terminKey = terminKey;
            this.datum = DateTime.Today;

            this.glavneTegobe = glavneTegobe;
            this.sadasnjaAnamneza = sadasnjaAnamneza;
            this.opstePojave = opstePojave;

            if (respiratorniSistem == "") this.respiratorniSistem = "/";
                else this.respiratorniSistem = respiratorniSistem;

            if (kardiovaskularniSistem == "") this.kardiovaskularniSistem = "/";
                else this.kardiovaskularniSistem = kardiovaskularniSistem;

            if (digestivniSistem == "") this.digestivniSistem = "/";
                else this.digestivniSistem = digestivniSistem;

            if (urogenitalniSistem == "") this.urogenitalniSistem = "/";
                else this.urogenitalniSistem = urogenitalniSistem;

            if (lokomotorniSistem == "") this.lokomotorniSistem = "/";
                else this.lokomotorniSistem = lokomotorniSistem;

            if (nervniSistem == "") this.nervniSistem = "/";
                else this.nervniSistem = nervniSistem;

            if (ranijaOboljenja == "") this.ranijaOboljenja = "/";
                else this.ranijaOboljenja = ranijaOboljenja;

            if (porodicniPodaci == "") this.porodicniPodaci = "/";
                else this.porodicniPodaci = porodicniPodaci;

            if (socioEpiPodaci == "") this.socioEpiPodaci = "/";
                else this.socioEpiPodaci = socioEpiPodaci;
            
        }

        public Termin getTermin()
        {
            return TerminStorage.Instance.Read(this.terminKey);
        }

        public String ImeLekara
        {
            get { return this.getTermin().ImeLekara; }
        }

        public String ImePacijenta
        {
            get { return this.getTermin().ImePacijenta; }
        }

        public String Date
        {
            get { return datum.ToString("dd.MM.yyyy."); }
        }

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
        public String GlavneTegobe
        {
            get => glavneTegobe;
            set => glavneTegobe = value;
        }

        public String SadasnjaAnamneza
        {
            get => sadasnjaAnamneza;
            set => sadasnjaAnamneza = value;
        }

        public String OpstePojave
        {
            get => opstePojave;
            set => opstePojave = value;
        }

        public String RespiratorniSistem
        {
            get => respiratorniSistem;
            set => respiratorniSistem = value;
        }

        public String KardiovaskularniSistem
        {
            get => kardiovaskularniSistem;
            set => kardiovaskularniSistem = value;
        }

        public String DigestivniSistem
        {
            get => digestivniSistem;
            set => digestivniSistem = value;
        }

        public String UrogenitalniSistem
        {
            get => urogenitalniSistem;
            set => urogenitalniSistem = value;
        }

        public String LokomotorniSistem
        {
            get => lokomotorniSistem;
            set => lokomotorniSistem = value;
        }

        public String NervniSistem
        {
            get => nervniSistem;
            set => nervniSistem = value;
        }

        public String RanijaOboljenja
        {
            get => ranijaOboljenja;
            set => ranijaOboljenja = value;
        }

        public String PorodicniPodaci
        {
            get => porodicniPodaci;
            set => porodicniPodaci = value;
        }

        public String SocioEpiPodaci
        {
            get => socioEpiPodaci;
            set => socioEpiPodaci = value;
        }

        public String AnamnezaKey
        {
            get => terminKey;
            set => terminKey = value;
        }

        public DateTime Datum
        {
            get => datum;
            set => datum = value;
        }
    }
}
