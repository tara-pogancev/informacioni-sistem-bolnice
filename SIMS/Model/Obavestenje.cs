using Newtonsoft.Json;
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
        public string Target { get; set; } 

        public Obavestenje()
        {
            
        }

        public Obavestenje(string autor, DateTime vreme, string tekst, string target)
        {
            Autor = autor;
            Vreme = vreme;
            Tekst = tekst;
            ID = autor+vreme.ToString("yyMMddHHmmss");
            Target = target;
        }

        [JsonIgnore]
        public string VremeStringDetailed
        {
            get
            {
                return Vreme.ToString("dd.MM.yyyy. HH:mm");
            }
        }

        [JsonIgnore]
        public String AutorUppercase
        {
            get { return Autor.ToUpper(); }
        }


        public String VremeString
        {
            get 
            {
                if (Vreme.Date == (DateTime.Today))
                    return Vreme.ToString("HH:mm");

                return Vreme.ToString("dd.MM.yyyy.");
            }
        }

       
    }
}
