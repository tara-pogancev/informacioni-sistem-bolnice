using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DinamickaOprema
    {
        public string naziv { get; set; }
        public string id { get; set; }
        public int kolicina { get; set; }

        public DinamickaOprema()
        {

        }
        public DinamickaOprema(string naziv, string id, int kolicina)
        {
            this.naziv = naziv;
            this.id = id;
            this.kolicina = kolicina;
        }
    }
}
