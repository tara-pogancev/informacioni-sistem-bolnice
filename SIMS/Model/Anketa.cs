using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Model
{
    public class Anketa
    {
        
        protected double ocjena;
        protected String komentar;

        public Anketa() { }

        public Anketa(double ocjena,String komentar)
        {
            this.ocjena = ocjena;
            this.komentar = komentar;
        }

        public double Ocjena { get => ocjena; set => ocjena = value; }
        public string Komentar { get => komentar; set => komentar = value; }
        
    }
}
