using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class Anketa
    {
        protected String idAnkete;
        protected String komentar;
        protected DateTime datumKreiranjaAnkete;
        protected String idVlasnika;

        public string IdAnkete { get => idAnkete; set => idAnkete = value; }
        
        public string Komentar { get => komentar; set => komentar = value; }
        public DateTime DatumKreiranjaAnkete { get => datumKreiranjaAnkete; set => datumKreiranjaAnkete = value; }
        public string IdVlasnika { get => idVlasnika; set => idVlasnika = value; }

        public Anketa() {
            datumKreiranjaAnkete = DateTime.Now;
        }

        public Anketa(String komentar,String idVlasnika)
        {
           
            this.komentar = komentar;
            this.idVlasnika = idVlasnika;
            datumKreiranjaAnkete = DateTime.Now;

        }

       
    }
}
