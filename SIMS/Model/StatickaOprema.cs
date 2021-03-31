using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class StatickaOprema
    {
        public string naziv { get; set; }
        public string id { get; set; }
        public int kolicina { get; set; }

        public StatickaOprema()
        {

        }
        public StatickaOprema(string naziv, string id, int kolicina)
        {
            this.naziv = naziv;
            this.id = id;
            this.kolicina = kolicina;
        }
    }
}
