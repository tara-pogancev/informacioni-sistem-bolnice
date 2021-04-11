using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Alergen
    {
        public string ID { get; set; }
        public string Naziv { get; set; }

        public Alergen()
        {

        }
        public Alergen(string ID, string Naziv)
        {
            this.ID = ID;
            this.Naziv = Naziv;
        }
    }
}
