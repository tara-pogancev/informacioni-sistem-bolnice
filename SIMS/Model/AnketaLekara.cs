using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class AnketaLekara : Anketa
    {
        private Termin termin;
        

        public AnketaLekara()
        {
        }

        public AnketaLekara(Termin termin,int ocjena,String komentar):base(ocjena,komentar)
        {
            this.termin = termin;
        }

        public Termin Termin { get => termin; set => termin = value; }
        
    }
}
