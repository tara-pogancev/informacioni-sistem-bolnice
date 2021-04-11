using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Obavestenje
    {
        public string Autor { get; set; }
        public DateTime Vreme { get; set; }
        public string Tekst { get; set; }
        public string ID { get; set; }

        public Obavestenje()
        {

        }

        public Obavestenje(string autor, DateTime vreme, string tekst)
        {
            Autor = autor;
            Vreme = vreme;
            Tekst = tekst;
            ID = autor+vreme.ToString("yyMMddHHmmss");
        }

        public string VremeString
        {
            get
            {
                return Vreme.ToString("dd/MM/yyyy HH:mm");
            }
        }
    }
}
