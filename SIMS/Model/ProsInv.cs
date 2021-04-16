using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model
{
    class ProsInv
    {
        public string BrojProstorije { get; set; }
        public string IdInventara { get; set; }
        public int Kolicina { get; set; }

        public ProsInv()
        {

        }
        public ProsInv(string brojProstorije, string idInventara, int kolicina)
        {
            this.BrojProstorije = brojProstorije;
            this.IdInventara = idInventara;
            this.Kolicina = kolicina;
        }
    }
}
