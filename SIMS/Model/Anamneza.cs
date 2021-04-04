using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace SIMS.Model
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
            this.respiratorniSistem = respiratorniSistem;
            this.kardiovaskularniSistem = kardiovaskularniSistem;
            this.digestivniSistem = digestivniSistem;
            this.urogenitalniSistem = urogenitalniSistem;
            this.lokomotorniSistem = lokomotorniSistem;
            this.nervniSistem = nervniSistem;
            this.ranijaOboljenja = ranijaOboljenja;
            this.porodicniPodaci = porodicniPodaci;
            this.socioEpiPodaci = socioEpiPodaci;

        }

        public Termin getTermin
        {
            get

            {
                return TerminStorage.Instance.Read(this.terminKey);
            }
        }

        public String ImeLekara
        {
            get { return this.getTermin.ImeLekara; }
        }

        public String ImePacijenta
        {
            get { return this.getTermin.ImePacijenta; }
        }

        public String Date
        {
            get { return datum.ToString("dd/MM/yyyy"); }
        }

        public String TerminDateType
        {
            get
            {

                if (getTermin.GetVrsta.Equals(TipTermina.pregled))
                    return "Datum pregleda: " + getTermin.Datum;
                else return "Datum operacije: " + getTermin.Datum;

            }
        }

        //TODO get set za sva polja
        public String GlavneTegobe
        {
            get => glavneTegobe;
        }

        public String SadasnjaAnamneza
        {
            get => sadasnjaAnamneza;
        }

        public String OpstePojave
        {
            get => opstePojave;
        }

        public String RespiratorniSistem
        {
            get => respiratorniSistem;
        }

        public String KardiovaskularniSistem
        {
            get => kardiovaskularniSistem;
        }

        public String DigestivniSistem
        {
            get => digestivniSistem;
        }

        public String UrogenitalniSistem
        {
            get => urogenitalniSistem;
        }

        public String LokomotorniSistem
        {
            get => lokomotorniSistem;
        }

        public String NervniSistem
        {
            get => nervniSistem;
        }

        public String RanijaOboljenja
        {
            get => ranijaOboljenja;
        }

        public String PorodicniPodaci
        {
            get => porodicniPodaci;
        }

        public String SocioEpiPodaci
        {
            get => socioEpiPodaci;
        }

        public String AnamnezaKey
        {
            get => terminKey;
        }

        public DateTime Datum
        {
            get => datum;
        }
    }
}
