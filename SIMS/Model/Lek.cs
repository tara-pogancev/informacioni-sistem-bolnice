using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Lek
    {
        public string ID { get; set; }

        public string Naziv { get; set; }

        public List<string> Alergeni { get; set; }

        public Lek()
        {

        }

        public Lek(string iD, string naziv, List<string> alergeni)
        {
            ID = iD;
            Naziv = naziv;
            Alergeni = alergeni;
        }
    }
}
