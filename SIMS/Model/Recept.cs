using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Recept
    {
        public Recept()
        {
            ReceptKey = DateTime.Now.ToString("yyMMddhhmmss");
        }

        public Recept(Lekar lekar, Pacijent pacijent, String nazivLeka, String kolicina, String dijagnoza)
        {
            Lekar = lekar;
            Pacijent = pacijent;
            NazivLeka = nazivLeka;
            Kolicina = kolicina;
            Dijagnoza = dijagnoza;

            ReceptKey = DateTime.Now.ToString("yyMMddhhmmss");
            Datum = DateTime.Today;

        }   

        [JsonIgnore]
        public String ImeLekara { get { return Lekar.ImePrezime; } }

        [JsonIgnore]
        public String ImePacijenta { get { return Pacijent.ImePrezime; } }

        [JsonIgnore]
        public String DateString { get { return Datum.ToString("dd.MM.yyyy."); } }

        [JsonIgnore]
        public String NameAndQuantity { get { return (NazivLeka + " " + Kolicina); } }

        public String NazivLeka { get; set; }
        public String Kolicina { get; set; }
        public String Dijagnoza { get; set; }
        public String ReceptKey { get; set; }

        public Lekar Lekar { get; set; }

        public Pacijent Pacijent { get; set; }

        public DateTime Datum { get; set; }

        public void InitData()
        {
            Lekar = LekarStorage.Instance.Read(Lekar.Jmbg);
        }

    }
}
