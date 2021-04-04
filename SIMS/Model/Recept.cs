using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace SIMS.Model
{
    public class Recept
    {
        private String receptKey;
        private String lekarKey;
        private String pacijentKey;
        private DateTime datum;
        private String nazivLeka;
        private String kolicina;
        private String dijagnoza;

        public Recept()
        {
            //this.receptKey = DateTime.Now.ToString("yyMMddhhmmss");
        }

        public Recept(String lekarKey, String pacijentKey, String nazivLeka, String kolicina, String dijagnoza)
        {
            this.lekarKey = lekarKey;
            this.pacijentKey = pacijentKey;
            this.nazivLeka = nazivLeka;
            this.kolicina = kolicina;
            this.dijagnoza = dijagnoza;

            this.receptKey = DateTime.Now.ToString("yyMMddhhmmss");
            this.datum = DateTime.Today;

        }   

        public String ImeLekara { get { return LekarStorage.Instance.Read(lekarKey).ImePrezime; } }

        public String ImePacijenta { get { return PacijentStorage.Instance.Read(pacijentKey).ImePrezime; } }

        public String DateString { get { return datum.ToString("dd/MM/yyyy"); } }

        public String NazivLeka { get => this.nazivLeka;  }
        public String Kolicina { get => this.kolicina; }
        public String Dijagnoza { get => this.dijagnoza; }
        public String ReceptKey { get => this.receptKey; }
        public String LekarKey { get => this.lekarKey; }
        public String PacijentKey { get => this.pacijentKey; }

    }
}
