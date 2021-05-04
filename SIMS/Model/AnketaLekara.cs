using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class AnketaLekara : Anketa
    {
        private Termin termin;
        private int ocjena;
        public AnketaLekara():base()
        {
        }

        public AnketaLekara(Termin termin,int ocjena,String komentar,String idVlasnika):base(komentar,idVlasnika)
        {
            this.termin = termin;
        }

        public Termin Termin { get => termin; set => termin = value; }
        public int Ocjena { get => ocjena; set => ocjena = value; }
    }
}
