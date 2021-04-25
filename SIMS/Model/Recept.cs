using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Recept
    {
        private String receptKey;
        private Lekar lekar;
        private Pacijent pacijent;
        private DateTime datum;
        private String nazivLeka;
        private String kolicina;
        private String dijagnoza;

        public Recept()
        {
        }

        public Recept(Lekar lekar, Pacijent pacijent, String nazivLeka, String kolicina, String dijagnoza)
        {
            this.lekar = lekar;
            this.pacijent = pacijent;
            this.nazivLeka = nazivLeka;
            this.kolicina = kolicina;
            this.dijagnoza = dijagnoza;

            this.receptKey = DateTime.Now.ToString("yyMMddhhmmss");
            this.datum = DateTime.Today;

        }   
        [JsonIgnore]
        public String ImeLekara { get { return lekar.ImePrezime; } }

        [JsonIgnore]
        public String ImePacijenta { get { return pacijent.ImePrezime; } }

        [JsonIgnore]
        public String DateString { get { return datum.ToString("dd.MM.yyyy."); } }

        public String NazivLeka { get => this.nazivLeka; set => nazivLeka = value; }
        public String Kolicina { get => this.kolicina; set => kolicina = value; }
        public String Dijagnoza { get => this.dijagnoza; set => dijagnoza = value; }
        public String ReceptKey { get => this.receptKey; set => receptKey = value; }
        public Lekar Lekar { get => lekar; set => lekar = value; }
        public Pacijent Pacijent { get => this.pacijent; set => pacijent = value; }

    }
}
